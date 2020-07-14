using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class TimeScaleViewModel
    {
        public virtual bool TimeScaleYear { get; set; } = false;
        public virtual bool TimeScaleQuarter { get; set; } = false;
        public virtual bool TimeScaleMonth { get; set; } = false;
        public virtual bool TimeScaleWeek { get; set; } = true;
        public virtual bool TimeScaleDay { get; set; } = false;
        public virtual bool TimeScaleWorkDay { get; set; } = true;
        public virtual bool TimeScaleHour { get; set; } = false;
        public virtual bool TimeScaleWorkHour { get; set; } = true;
    }
}
