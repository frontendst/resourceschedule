using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class TaskViewModel : ISchedulerEvent
    {
        public TaskViewModel() { }

        public int TaskViewModelID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceException { get; set; }

        public string RecurrenceRule { get; set; }

        public int ProgrammerID { get; set; }

        public int ProjectID { get; set; }

        public int? SpecializationID { get; set; }

        public string Color { get; set; }

        public float Charge { get; set; }

        public TaskViewModel(Task task)
        {
            TaskViewModelID = task.TaskID;
            Start = task.StartTime;
            End = task.EndTime;
            StartTimezone = "";
            EndTimezone = "";
            IsAllDay = false;
            RecurrenceException = null;
            RecurrenceRule = null;
            ProgrammerID = task.ProgrammerID;
            ProjectID = task.ProjectID;
            Charge = task.Charge;
        }

        public Task ToEntity()
        {
            var programmer = new Programmer();
            using (var context = new DbContext())
            {
                programmer = context.Programmers.Find(ProgrammerID);
            }

            var task = new Task
            {
                TaskID = TaskViewModelID,
                StartTime = Start,
                EndTime = End,
                Programmer = programmer,
                ProgrammerID = this.ProgrammerID,
                ProjectID = this.ProjectID,
                Charge = this.Charge
            };
            return task;
        }
    }
}