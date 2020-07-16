using DevExpress.Xpf.Scheduling;
using System;
using System.Linq;
using System.Windows.Media;
using TestScheduler.ViewModels;

namespace TestScheduler.Converters
{
    public class UserToResourceItemConverter : IOneWayConverter<UserViewModel, ResourceItem>
    {
        public ResourceItem Convert(UserViewModel input)
        {
            var res = new ResourceItem
            {
                Caption = input.Name,
                Visible = true,
                Id = input.Id,
                Brush = new BrushConverter().ConvertFromString(input.Color) as Brush
            };

            return res;
        }
    }
}
