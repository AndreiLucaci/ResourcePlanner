using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using DevExpress.Mvvm.Gantt;
using TestScheduler.ViewModels;

namespace TestScheduler.Converters
{
    public class ExpandoToTaskViewModelConverter : IOneWayConverter<ExpandoObject, TaskViewModel>
    {
        public TaskViewModel Convert(ExpandoObject input)
        {
            var dictionary = input as IDictionary<string, object>;

            var TaskViewModel = new TaskViewModel
            {
                StartTime = System.Convert.ToDateTime(dictionary["StartDateTime"]),
                EndTime = System.Convert.ToDateTime(dictionary["FinishDateTime"]),
                Subject = System.Convert.ToString(dictionary["ShownText"]),
                Id = System.Convert.ToInt32(dictionary["Id"]),
                Notes = System.Convert.ToString(dictionary["ToolTipText"]),
                Progress = System.Convert.ToDouble(dictionary["Completed"]),
            };

            return TaskViewModel;
        }
    }
}
