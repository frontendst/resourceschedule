using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class ProjectViewModel : ISchedulerEvent
    {
        public ProjectViewModel() { }

        public int ProjectViewModelID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public bool IsAllDay { get; set; }
        
        public string RecurrenceException { get; set; }
        
        public string RecurrenceRule { get; set; }

        public string Color { get; set; }

        public IEnumerable<int> Programmers { get; set; }

        public int? RecurrenceId { get; set; }

        public int? WorkType { get; set; }

        public ProjectViewModel(Project project)
        {
            Title = project.Name;
            Description = project.Description;
            Start = project.StartTime;
            End = project.EndTime;
            StartTimezone = "";
            EndTimezone = "";
            IsAllDay = false;
            RecurrenceException = "";
            RecurrenceRule = "";
            Color = project.Color;
            Programmers = project.Programmers.Select(x => x.ProgrammerID);

        }

        public Project ToEntity()
        {
            return new Project
            {
                Name = Title,
                Description = this.Description,
                StartTime = Start,
                EndTime = End,
                Color = this.Color,
            };
        }

        public static ProjectViewModel[] ToViewArray(Project[] projects)
        {
            var viewList = new ProjectViewModel[projects.Count()];
            int i = 0;
            foreach(var p in projects)
            {
                viewList[i] = new ProjectViewModel(p);
                i++;
            }
            return viewList;
        }
       
    }
}