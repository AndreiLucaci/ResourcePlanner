using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Visual;
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
            var myControl = (TreeListView)(this.Resources["myTemplate"] as ControlTemplate).FindName("treeListView", ResourceTree);
            LeftScroll = LayoutTreeHelper.GetVisualChildren(myControl).OfType<ScrollViewer>().FirstOrDefault();
            RightScroll = LayoutTreeHelper.GetVisualChildren(scheduler).OfType<SchedulerScrollViewer>().FirstOrDefault();

            if (LeftScroll != null && RightScroll != null)
            {
                LeftScroll.Name = "LeftScroll";
                LeftScroll.ScrollChanged += ScrollOwner_ScrollChanged;
                RightScroll.Name = "RightScroll";
                RightScroll.ScrollChanged += ScrollOwner_ScrollChanged;
            }

            SetSameResourceColors();
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
            var cellContainers = ((DevExpress.Xpf.Scheduling.VisualData.TimelineViewVisualDataBase)timelineView.VisualData).CellContainers;

            foreach (var cellContainer in cellContainers)
            {
                var nrOfAppointments = cellContainer.Appointments.Count();
                if (nrOfAppointments >= 0 && DataContext is SchedulerViewModel model && !cellContainer.Resource.Id.Equals(EmptyResourceId.Id))
                {
                    var res = model.Users.Single(x => x.Id == (int)cellContainer.Resource.Id && x.Name == cellContainer.Resource.Caption);

                    res.RowHeight =
                        nrOfAppointments == 0
                            ? model.SelectedRowHeight.Height - 1
                            : (model.SelectedRowHeight.Height - 2) * nrOfAppointments + 11 + nrOfAppointments;
                }
            }
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
