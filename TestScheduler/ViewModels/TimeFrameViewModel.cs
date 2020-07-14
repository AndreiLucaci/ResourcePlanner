using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class TimeFrameViewModel
    {
        public static TimeFrameViewModel Daily = new TimeFrameViewModel
        {
            Name = "Daily",
            TimeFrame = ViewModels.TimeFrames.Daily
        };

        public static TimeFrameViewModel Weekly = new TimeFrameViewModel
        {
            Name = "Weekly",
            TimeFrame = ViewModels.TimeFrames.Weekly
        };

        public static TimeFrameViewModel BiWeekly = new TimeFrameViewModel
        {
            Name = "BiWeekly",
            TimeFrame = ViewModels.TimeFrames.BiWeekly
        };

        public static TimeFrameViewModel Monthly = new TimeFrameViewModel
        {
            Name = "Monthly",
            TimeFrame = ViewModels.TimeFrames.Monthly
        };

        public static TimeFrameViewModel Yearly = new TimeFrameViewModel
        {
            Name = "Yearly",
            TimeFrame = ViewModels.TimeFrames.Yearly
        };


        public virtual TimeFrames TimeFrame { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum TimeFrames
    {
        Daily,
        Weekly,
        BiWeekly,
        Monthly,
        Yearly
    }
}
