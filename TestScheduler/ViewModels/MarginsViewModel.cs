using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class MarginsViewModel : INotifyPropertyChanged
    {
        private int TimeFrameRowHeight = 25;
        private int TopMarginPadding = 9;
        private int LeftHeaderHeight = 20;


        private string leftGridMargin;
        public virtual string LeftGridMargin
        {
            get => leftGridMargin; set
            {

                leftGridMargin = value;
            }
        }

        public void ComputeLeftGridMargin(TimeScaleViewModel timeScale)
        {
            var nrOfRows = GetNumberOfRows(timeScale);

            var maxHeight = (nrOfRows * TimeFrameRowHeight) + TopMarginPadding;
            var neededMargin = maxHeight - LeftHeaderHeight + (nrOfRows > 1 ? nrOfRows - 1 : 0);

            var margin = $"0 {neededMargin} 0 0";

            LeftGridMargin = margin;
        }

        private int GetNumberOfRows(TimeScaleViewModel viewModel)
        {
            var nr = 0;
            if (viewModel.TimeScaleDay || viewModel.TimeScaleWorkDay)
            {
                nr++;
            }

            if (viewModel.TimeScaleHour || viewModel.TimeScaleWorkHour)
            {
                nr++;
            }

            if (viewModel.TimeScaleMonth)
            {
                nr++;
            }

            if (viewModel.TimeScaleQuarter)
            {
                nr++;
            }

            if (viewModel.TimeScaleWeek)
            {
                nr++;
            }

            if (viewModel.TimeScaleYear)
            {
                nr++;
            }

            return nr;
        }



        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
