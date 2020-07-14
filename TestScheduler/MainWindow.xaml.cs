using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduling;
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
        IScrollInfo TopScrollElement { get; set; }
        IScrollInfo BottomScrollElement { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LayoutUpdated += MainWindow_LayoutUpdated;
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

        void MainWindow_LayoutUpdated(object sender, EventArgs e)
        {
            //var template = this.Resources["myTemplate"] as ControlTemplate;
            //var myControl = (TableView)template.FindName("gridTable", ResourceTree);
            //TopScrollElement = (DataPresenter)LayoutHelper.FindElement(LayoutHelper.FindElementByName(myControl, "PART_ScrollContentPresenter"), (el) => el is DataPresenter);
            //BottomScrollElement = (DataPresenter)LayoutHelper.FindElement(LayoutHelper.FindElementByName(scheduler, "PART_ScrollContentPresenter"), (el) => el is DataPresenter);
            //TopScrollElement.ScrollOwner.Name = "TopScroll";
            //BottomScrollElement.ScrollOwner.Name = "BottomScroll";
            //TopScrollElement.ScrollOwner.ScrollChanged += ScrollOwner_ScrollChanged;
            //BottomScrollElement.ScrollOwner.ScrollChanged += ScrollOwner_ScrollChanged;
        }

        void ScrollOwner_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrv = sender as ScrollViewer;
            if (scrv.Name == "TopScroll")
                BottomScrollElement.SetVerticalOffset(TopScrollElement.VerticalOffset);
            else
                TopScrollElement.SetVerticalOffset(BottomScrollElement.VerticalOffset);
        }
    }
}
