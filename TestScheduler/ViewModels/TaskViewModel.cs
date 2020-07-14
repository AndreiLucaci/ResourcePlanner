using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class TaskViewModel
    {
        public static TaskViewModel Create()
        {
            return ViewModelSource.Create(() => new TaskViewModel());
        }

        internal static TaskViewModel Create(DateTime startTime, DateTime endTime, string notes, string location, string taskName, int userId)
        {
            var model = Create();

            model.StartTime = startTime;
            model.EndTime = endTime;
            model.Notes = notes;
            model.Location = location;
            model.TaskName = taskName;
            model.UserId = userId;

            return model;
        }

        public virtual int Id { get; set; }
        public virtual bool AllDay { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual string TaskName { get; set; }
        public virtual string Notes { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Location { get; set; }
        public virtual int UserId { get; set; }
        public virtual int Type { get; set; }
        public virtual double Progress { get; set; }
    }
}
