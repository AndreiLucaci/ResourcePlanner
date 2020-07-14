using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class RowHeightViewModel
    {
        public virtual int Height { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static RowHeightViewModel Single = new RowHeightViewModel
        {
            Height = 40,
            Name = "Single row"
        };

        public static RowHeightViewModel OnePointFive = new RowHeightViewModel
        {
            Height = 60,
            Name = "1.5 row"
        };

        public static RowHeightViewModel Double = new RowHeightViewModel
        {
            Height = 80,
            Name = "Double row"
        };
    }
}
