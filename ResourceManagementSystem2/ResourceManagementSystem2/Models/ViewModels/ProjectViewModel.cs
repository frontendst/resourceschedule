using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel() { }

        public int ProjectViewModelID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public string Color { get; set; }

        public IEnumerable<int> Tasks { get; set; }

        public ProjectViewModel(Project project)
        {
            ProjectViewModelID = project.ProjectID;
            Title = project.Name;
            Description = project.Description;
            Color = (project.Color != null && project.Color != "")? project.Color : "#ffffff";
            Tasks = null;
        }

        public Project ToEntity()
        {
            var project = new Project
            {
                ProjectID = ProjectViewModelID,
                Name = Title,
                Description = this.Description,
                Color = this.Color
            };

            var resultTasksList = new List<Task>();
            using (var context = new DbContext())
            {
                var tasks = context.Tasks;
                foreach(var num in Tasks)
                {
                    resultTasksList.Add(tasks.Find(num));
                }
            }

            project.Tasks = resultTasksList;

            return project;
        }
    }
}