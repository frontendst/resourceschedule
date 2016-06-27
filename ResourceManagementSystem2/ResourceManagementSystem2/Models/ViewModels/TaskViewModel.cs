using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public IEnumerable<int> Programmers { get; set; }

        public IEnumerable<int> Projects { get; set; }

        public string Color { get; set; }


        public TaskViewModel(Task task)
        {
            TaskViewModelID = task.TaskID;
            Title = task.Text;
            Description = task.Text;
            Start = task.StartTime;
            End = task.EndTime;
            StartTimezone = "";
            EndTimezone = "";
            IsAllDay = false;
            RecurrenceException = null;
            RecurrenceRule = null;
            Programmers = new int[] { task.ProgrammerID};
            Projects = new int[] { task.ProjectID };
            using (var context = new DbContext())
            {
                Color = context.Projects.Find(task.ProjectID).Color;
            }
        }

        public Task ToEntity()
        {
            var programmer = new Programmer();
            using (var context = new DbContext())
            {
                programmer = context.Programmers.Find(Programmers.First());
            }

            var task = new Task
            {
                TaskID = TaskViewModelID,
                Text = Title,
                StartTime = Start,
                EndTime = End,
                Programmer = programmer,
                ProgrammerID = this.Programmers.First(),
                ProjectID = this.Projects.First()
            };
            return task;
        }
    }
}