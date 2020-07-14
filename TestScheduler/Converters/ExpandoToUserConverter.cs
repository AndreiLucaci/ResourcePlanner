using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.Gantt;
using TestScheduler.ViewModels;

namespace TestScheduler.Converters
{
    public class ExpandoToUserConverter : IOneWayConverter<ExpandoObject, UserViewModel>
    {
        public UserViewModel Convert(ExpandoObject input)
        {
            var dic = input as IDictionary<string, object>;
            var res = new UserViewModel
            {
                Id = System.Convert.ToInt32(dic["Id"]),
                Color = System.Convert.ToString(dic["Color"]),
                Name = System.Convert.ToString(dic["Name"]),
                Department = System.Convert.ToString(dic["Department"]),
            };

            return res;
        }
    }
}
