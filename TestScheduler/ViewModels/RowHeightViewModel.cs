using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class RowHeightViewModel
    {
        public virtual int AppointmentHeight { get => Height; }
        public virtual int Height { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static readonly RowHeightViewModel Single = new RowHeightViewModel
        {
            Height = 40,
            Name = "Single row"
        };

        public static readonly RowHeightViewModel OnePointFive = new RowHeightViewModel
        {
            Height = 60,
            Name = "1.5 row"
        };

        public static readonly RowHeightViewModel Double = new RowHeightViewModel
        {
            Height = 80,
            Name = "Double row"
        };

        public static readonly RowHeightViewModel Thin = new RowHeightViewModel
        {
            Height = 20,
            Name = "Thin row"
        };
    }
}
