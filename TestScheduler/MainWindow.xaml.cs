using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Visual;
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

        public MainWindow()
        {
            InitializeComponent();
            timelineView.Loaded += TimelineView_Loaded;
        }

        private void TimelineView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var myControl = (TableView)(this.Resources["myTemplate"] as ControlTemplate).FindName("gridTable", ResourceTree);
            LeftScroll = LayoutTreeHelper.GetVisualChildren(myControl).OfType<ScrollViewer>().FirstOrDefault();
            RightScroll = LayoutTreeHelper.GetVisualChildren(scheduler).OfType<SchedulerScrollViewer>().FirstOrDefault();

            if (LeftScroll != null && RightScroll != null)
            {
                LeftScroll.Name = "LeftScroll";
                LeftScroll.ScrollChanged += ScrollOwner_ScrollChanged;
                RightScroll.Name = "RightScroll";
                RightScroll.ScrollChanged += ScrollOwner_ScrollChanged;
            }
        }

        void ScrollOwner_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrv = sender as ScrollViewer;
            if (scrv.Name == "LeftScroll")
                RightScroll.ScrollToVerticalOffset(LeftScroll.VerticalOffset);
            else
                LeftScroll.ScrollToVerticalOffset(RightScroll.VerticalOffset);
        }

        private void BarEditItem_EditValueChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ScheduleViewModel model)
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
    }
}
