using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResourceManagementSystem2.Models
{
    public class TaskService
    {
        private readonly DbContext context;

        public TaskService(DbContext context)
        {
            this.context = context;
        }

        public TaskService()
        {
            context = new DbContext();
        }

        public IQueryable<TaskViewModel> GetAll()
        {
            var taskList = context.Tasks.ToList();
            var taskViewList = new List<TaskViewModel>();
            foreach (var t in taskList)
            {
                taskViewList.Add(new TaskViewModel(t));
            }

            return taskViewList.AsQueryable();
        }

        public void Insert(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (!ValidateModel(task, modelState))
                return;

            if (string.IsNullOrEmpty(task.Title))
            {
                task.Title = "";
            }

            if (string.IsNullOrEmpty(task.Description))
            {
                task.Description = "";
            }

            var project = context.Projects.Find(task.ProjectID);
            task.Color = project.Color;
            task.Title = project.Name;

            var entity = task.ToEntity();
            //entity.Project = context.Projects.Find(task.Projects.First());
            entity.Project = context.Projects.Find(task.ProjectID);
            //entity.Programmer = context.Programmers.Find(task.Programmers.First());
            entity.Programmer = context.Programmers.Find(task.ProgrammerID);
            entity.ProjectID = entity.Project.ProjectID;
            entity.ProgrammerID = entity.Programmer.ProgrammerID;
            context.Tasks.Add(entity);
            context.SaveChanges();
            task.TaskViewModelID = entity.TaskID;

        }

        public virtual void Update(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (!ValidateModel(task, modelState))
                return;

            if (string.IsNullOrEmpty(task.Title))
            {
                task.Title = "";
            }

            var original = context.Tasks.Find(task.TaskViewModelID);
            var entityTask = task.ToEntity();
            if (original != null)
            {
                original.StartTime = entityTask.StartTime;
                original.EndTime = entityTask.EndTime;
                original.Programmer = context.Programmers.Find(entityTask.ProgrammerID);
                original.Project = context.Projects.Find(entityTask.ProjectID);
                original.Text = entityTask.Text;
            }
            context.SaveChanges();
        }

        public virtual void Delete(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (!ValidateModel(task, modelState))
                return;
            context.Tasks.Remove(context.Tasks.First(x => x.TaskID == task.TaskViewModelID));
            context.SaveChanges();
        }

        private static bool ValidateModel(TaskViewModel appointment, ModelStateDictionary modelState)
        {
            if (appointment.Start <= appointment.End)
                return true;
            modelState.AddModelError("errors", "End date must be greater or equal to Start date.");
            return false;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}