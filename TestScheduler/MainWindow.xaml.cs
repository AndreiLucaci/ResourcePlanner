using DevExpress.Data.Filtering;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Visual;
using DevExpress.Xpf.Scheduling.VisualData;
using DevExpress.XtraScheduler;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TestScheduler.ViewModels;

namespace TestScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private ScrollViewer LeftScroll { get; set; }

        private ScrollViewer RightScroll { get; set; }

        public DelegateCommand RowSizeClickCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            timelineView.Loaded += TimelineView_Loaded;
            RowSizeClickCommand = new DelegateCommand(SyncRowSizes);
            DataContextChanged += MainWindow_DataContextChanged;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            AttachToModelPropertyChanges();
            if (DataContext is SchedulerViewModel model)
            {
                model.View = scheduler;
                //var nodes = model.GenerateTree();
                //var myControl = (TreeListView)(this.Resources["myTemplate"] as ControlTemplate).FindName("treeListView", ResourceTree);
                //myControl.Nodes.Clear();
                //nodes.ForEach(x => myControl.Nodes.Add(x));
            }
        }

        private void MainWindow_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            AttachToModelPropertyChanges();
        }

        private void AttachToModelPropertyChanges()
        {
            if (DataContext is SchedulerViewModel model)
            {
                model.PropertyChanged += (a, o) =>
                {
                    if (o.PropertyName == nameof(model.SelectedRowHeight))
                    {
                        SyncRowSizes();
                    }
                };
            }
        }

        private void TimelineView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var gridControl = (GridControl)(this.Resources["myTemplate"] as ControlTemplate).FindName("gridcontrol", ResourceTree);
            var treeListView = (TreeListView)(this.Resources["myTemplate"] as ControlTemplate).FindName("treeListView", ResourceTree);
            LeftScroll = LayoutTreeHelper.GetVisualChildren(treeListView).OfType<ScrollViewer>().FirstOrDefault();
            RightScroll = LayoutTreeHelper.GetVisualChildren(scheduler).OfType<SchedulerScrollViewer>().FirstOrDefault();

            if (LeftScroll != null && RightScroll != null)
            {
                LeftScroll.Name = "LeftScroll";
                LeftScroll.ScrollChanged += ScrollOwner_ScrollChanged;
                RightScroll.Name = "RightScroll";
                RightScroll.ScrollChanged += ScrollOwner_ScrollChanged;
            }

            SetSameResourceColors();
            HideRoot(gridControl, treeListView);
        }

        private void HideRoot(GridControl gridControl, TreeListView treeListView)
        {
            gridControl.ItemsSource = (DataContext as SchedulerViewModel)?.Users;
            treeListView.CustomNodeFilter += CustomNodeFilter;
            gridControl.FilterCriteria = CriteriaOperator.Parse("true");
        }

        private void CustomNodeFilter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeFilterEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Visible = false;
                e.Node.IsExpanded = true;
                e.Handled = true;
            }
        }

        private void SetSameResourceColors()
        {
            var resources = timelineView.Scheduler.ResourceItems;
            if (resources != null && DataContext is SchedulerViewModel model)
            {
                foreach (var res in resources)
                {
                    var usr = model.Users.Single(x => x.Id == (int)res.Id && x.Name == res.Caption);
                    usr.Color = res.Color.ToString();
                }
            }
        }

        private void SyncRowSizes()
        {
            var appointmentsContainerPanels =
                LayoutTreeHelper
                    .GetVisualChildren(scheduler)
                    .OfType<DevExpress.Xpf.Scheduling.Panels.TimelineAppointmentsContainerPanel>()
                    //.OfType<DevExpress.Xpf.Scheduling.Panels.TimeRegionDecorationPanel>()
                    .ToList();

            foreach (var appointmentContainerPanel in appointmentsContainerPanels)
            {
                if (appointmentContainerPanel.DataContext is TimelineCellContainerViewModel dtCtx &&
                    DataContext is SchedulerViewModel model &&
                    (!dtCtx.Resource?.Id.Equals(EmptyResourceId.Id)).GetValueOrDefault())
                {
                    var res = model.Users.Single(x => x.Id == (int)dtCtx.Resource.Id && x.Name == dtCtx.Resource.Caption);
                    res.RowHeight =
                        appointmentContainerPanel.ActualHeight +
                        appointmentContainerPanel.Margin.Bottom +
                        appointmentContainerPanel.Margin.Top;
                }
            }
        }

        private static int ComputeNumberOfAppointments(TimelineCellContainerViewModelBase cellContainer)
        {
            var allAppointments = cellContainer.Appointments.Count(x => x.Appointment.Duration != default);
            var onTheSameLineAppointments = 0;
            for (var i = 0; i < cellContainer.Appointments.Count(); i++)
            {
                for (var j = i; j < cellContainer.Appointments.Count(); j++)
                {
                    if (i != j)
                    {
                        var ap1 = cellContainer.Appointments.ElementAt(i);
                        var ap2 = cellContainer.Appointments.ElementAt(j);

                        if (
                            cellContainer.Interval.Contains(ap1.Appointment.End)
                            && cellContainer.Interval.Contains(ap2.Appointment.Start)
                            && ap1.Appointment.End < ap2.Appointment.Start)
                        {
                            onTheSameLineAppointments++;
                        }
                    }
                }
            }

            //onTheSameLineAppointments =
            //    (onTheSameLineAppointments == default || onTheSameLineAppointments % 2 == 0)
            //        ? onTheSameLineAppointments
            //        : onTheSameLineAppointments - 1; // appointments on the same line come in pairs??

            return allAppointments - onTheSameLineAppointments;
        }

        void ScrollOwner_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrv = sender as ScrollViewer;
            if (scrv.Name == "LeftScroll")
                RightScroll.ScrollToVerticalOffset(LeftScroll.VerticalOffset);
            else
                LeftScroll.ScrollToVerticalOffset(RightScroll.VerticalOffset);
            SyncRowSizes();
        }

        private void BarEditItem_EditValueChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is SchedulerViewModel model)
            {
                model.RefreshTimeFrameSelection();
            }
        }

        private void GridControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is GridControl grid)
            {
                grid.GroupBy("Department");
                grid.ExpandAllGroups();
            }
        }

        private void scheduler_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {

        }

        private void scheduler_DependencyPropertyChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {

        }

        private void gridcontrol_GroupRowExpanding(object sender, RowAllowEventArgs e)
        {
            if (e.Row != null && e.Row is UserViewModel user && DataContext is SchedulerViewModel model)
            {
                model.Users.Where(x => x.Department == user.Department).ToList().ForEach(x => x.IsVisible = true);
            }
        }

        private void gridcontrol_GroupRowCollapsing(object sender, RowAllowEventArgs e)
        {
            if (e.Row != null && e.Row is UserViewModel user && DataContext is SchedulerViewModel model)
            {
                model.Users.Where(x => x.Department == user.Department).ToList().ForEach(x => x.IsVisible = false);
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SyncRowSizes();
        }

        private void scheduler_ItemsCollectionChanged(object sender, ItemsCollectionChangedEventArgs e)
        {
            if (e.ItemType == ItemType.AppointmentItem)
            {
                //SyncRowSizes();
            }
        }

        private void treeListView_NodeCollapsing(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs e)
        {
            if (e?.Row is UserViewModel user && DataContext is SchedulerViewModel model)
            {
                model.Users.Where(x => x.Department == user.Department && !x.Equals(user)).ToList().ForEach(x => x.IsVisible = false);
                var cellContainers = ((TimelineViewVisualDataBase)timelineView.VisualData).CellContainers;
                //cellContainers.Where(x => x.Resource.SourceObject.Equals(user)).ToList().SelectMany(x => x.Appointments).ToList().ForEach(x => x.Appointment.)
            }
        }

        private void treeListView_NodeExpanding(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs e)
        {
            if (e?.Row is UserViewModel user && DataContext is SchedulerViewModel model)
            {
                model.Users.Where(x => x.Department == user.Department && !x.Equals(user)).ToList().ForEach(x => x.IsVisible = true);
            }
        }
    }
}
