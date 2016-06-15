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

        public List<ProjectViewModel> GetAll()
        {
            var projectList = context.Projects.ToList();
            var projectViewList = new List<ProjectViewModel>();
            foreach (var a in projectList)
            {
                projectViewList.Add(new ProjectViewModel(a));
            }
            var programmerIntArray = projectList.Select(x => x.ProjectID).ToArray();



            return projectViewList;
        }

        public virtual void Insert(ProjectViewModel project, ModelStateDictionary modelState)
        {
            if (ValidateModel(project, modelState))
            {
                if (project.Programmers == null)
                {
                    project.Programmers = new int[0];//менял
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
                foreach(var oth in otherProgrammers)
                {
                    foreach(var p in programmers)
                    {
                        if (oth == p)
                        {
                            oth.Projects.Add(entity);
                            context.SaveChanges();
                        }
                    }
                }
                context.SaveChanges();
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

            var proj = context.Projects.Where(c => c.ProjectID == project.ProjectViewModelID).FirstOrDefault();

            var usedProgrammers = proj.Programmers.Where(c => c.Projects.Contains(proj)).ToList();

            usedProgrammers.ForEach(c => c.Projects.Remove(proj));
            context.SaveChanges();
            proj = project.ToEntity();
            usedProgrammers.ForEach(c => c.Projects.Add(proj));
            context.SaveChanges();
        }

        public virtual void Delete(ProjectViewModel project, ModelStateDictionary modelState)
        {
            if (project.Programmers == null)
            {
                project.Programmers = new int[0];//менял
            }

            context.Projects.Remove(project.ToEntity());
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