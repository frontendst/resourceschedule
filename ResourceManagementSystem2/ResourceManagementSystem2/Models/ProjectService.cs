using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;

namespace ResourceManagementSystem2.Models
{
    public class ProjectService : ISchedulerEventService<ProjectViewModel>
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

        public void Insert(ProjectViewModel project, ModelStateDictionary modelState)
        {
            if (ValidateModel(project, modelState))
            {
                if (string.IsNullOrEmpty(project.Title))
                {
                    project.Title = "";
                }

                if (project.Programmers == null)
                {
                    project.Programmers = new int[0];
                    context.Projects.Add(project.ToEntity());
                    context.SaveChanges();
                    return;
                }

                if (string.IsNullOrEmpty(project.Title))
                {
                    project.Title = "";
                }
                var entity = project.ToEntity();
                var programmerNums = project.Programmers;
                var programmers = new List<Programmer>();
                foreach (var p in programmerNums)
                {
                    programmers.Add(context.Programmers.Find(p));
                }
                entity.Programmers = programmers;

                var otherProgrammers = context.Programmers.ToList();
                foreach (var oth in otherProgrammers)
                {
                    foreach (var p in programmers)
                    {
                        if (oth == p)
                        {
                            oth.Projects.Add(entity);
                            context.SaveChanges();
                        }
                    }
                }
                context.SaveChanges();
                project.ProjectViewModelID = entity.ProjectID;
            }
        }

        public virtual void Update(ProjectViewModel project, ModelStateDictionary modelState)
        {
            if (!ValidateModel(project, modelState))
                return;

            if (string.IsNullOrEmpty(project.Title))
            {
                project.Title = "";
            }

            if (project.Programmers == null)
            {
                project.Programmers = new int[0];
            }

            var programmersOfUpdProjectsIds = project.Programmers;
            var programmersOfUpdProjects = new List<Programmer>();
            foreach (var p in programmersOfUpdProjectsIds)
            {
                programmersOfUpdProjects.Add(context.Programmers.Find(p));
            }

            var original = context.Projects.Find(project.ProjectViewModelID);

            if (original != null)
            {
                original.Programmers.Clear();
                original.Programmers.AddRange(programmersOfUpdProjects);
                original.Name = project.Title;
                original.StartTime = project.Start;
                original.EndTime = project.End;
                original.Color = project.Color;
                original.Description = project.Description;
            }
            context.SaveChanges();
        }

        public virtual void Delete(ProjectViewModel project, ModelStateDictionary modelState)
        {
            if (project.Programmers == null)
            {
                project.Programmers = new int[0];
            }

            context.Projects.Remove(context.Projects.First(x => x.ProjectID == project.ProjectViewModelID));
            context.SaveChanges();
        }

        private static bool ValidateModel(ISchedulerEvent appointment, ModelStateDictionary modelState)
        {
            if (appointment.Start <= appointment.End) return true;
            modelState.AddModelError("errors", "End date must be greater or equal to Start date.");
            return false;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}