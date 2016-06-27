using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;

namespace ResourceManagementSystem2.Models
{
    public class ProjectService
    {
        private readonly DbContext context;

        public ProjectService(DbContext context)
        {
            this.context = context;
        }

        public ProjectService()
        {
            context = new DbContext();
        }

        public IQueryable<ProjectViewModel> GetAll()
        {
            var projectList = context.Projects.ToList();
            var projectViewList = new List<ProjectViewModel>();
            foreach (var p in projectList)
            {
                projectViewList.Add(new ProjectViewModel(p));
            }

            return projectViewList.AsQueryable();
        }

        public void Insert(ProjectViewModel project)
        {

            if (string.IsNullOrEmpty(project.Title))
            {
                project.Title = "";
            }

            if (project.Tasks == null)
            {
                project.Tasks = new int[0];
                var entityProject = project.ToEntity();
                context.Projects.Add(entityProject);
                context.SaveChanges();
                project.ProjectViewModelID = entityProject.ProjectID;
                return;
            }

            var entity = project.ToEntity();
            context.SaveChanges();
            project.ProjectViewModelID = entity.ProjectID;

        }

        public void Update(ProjectViewModel project)
        {

            if (string.IsNullOrEmpty(project.Title))
            {
                project.Title = "";
            }

            if (project.Tasks == null)
            {
                project.Tasks = new int[0];
            }

            var tasksOfUpdProjectsIds = project.Tasks;
            var tasksOfUpdProjects = new List<Task>();
            foreach (var t in tasksOfUpdProjectsIds)
            {
                tasksOfUpdProjects.Add(context.Tasks.Find(t));
            }

            var original = context.Projects.Find(project.ProjectViewModelID);

            if (original != null)
            {
                original.Name = project.Title;
                original.Color = project.Color;
                original.Description = project.Description;
            }
            context.SaveChanges();
        }

        public void Delete(ProjectViewModel project)
        {
            if (project.Tasks == null)
            {
                project.Tasks = new int[0];
            }

            context.Projects.Remove(context.Projects.First(x => x.ProjectID == project.ProjectViewModelID));
            context.SaveChanges();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}