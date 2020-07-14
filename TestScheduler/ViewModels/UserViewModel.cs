using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class UserViewModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
        public virtual string Department { get; set; }
    }
}
