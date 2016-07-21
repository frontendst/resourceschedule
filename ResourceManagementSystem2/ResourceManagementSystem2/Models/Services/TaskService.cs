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

        public IQueryable<TaskViewModel> GetAll(int month, int year)
        {
            IQueryable<TaskViewModel> list = null;
            if (month == 0)
            {
                list = from tasks in context.Tasks
                       join projects in context.Projects on tasks.ProjectID equals projects.ProjectID
                       join programmers in context.Programmers on tasks.ProgrammerID equals programmers.ProgrammerID
                       select new TaskViewModel()
                       {
                           Color = projects.Color,
                           TaskViewModelID = tasks.TaskID,
                           Title = projects.Name,
                           Start = tasks.StartTime,
                           End = tasks.EndTime,
                           StartTimezone = "",
                           EndTimezone = "",
                           IsAllDay = false,
                           RecurrenceException = null,
                           RecurrenceRule = null,
                           ProgrammerID = tasks.ProgrammerID,
                           ProjectID = tasks.ProjectID,
                           SpecializationID = programmers.SpecializationID,
                           Charge = tasks.Charge

                       };
            }
            else
            {
               list = from tasks in context.Tasks
                          join projects in context.Projects on tasks.ProjectID equals projects.ProjectID
                          join programmers in context.Programmers on tasks.ProgrammerID equals programmers.ProgrammerID
                          where ((tasks.StartTime.Month <= month) && (tasks.EndTime.Month >= month) && (tasks.StartTime.Year == tasks.EndTime.Year))
                          select new TaskViewModel()
                          {
                                Color = projects.Color,
                                TaskViewModelID = tasks.TaskID,
                                Title = projects.Name,
                                Start = tasks.StartTime,
                                End = tasks.EndTime,
                                StartTimezone = "",
                                EndTimezone = "",
                                IsAllDay = false,
                                RecurrenceException = null,
                                RecurrenceRule = null,
                                ProgrammerID = tasks.ProgrammerID,
                                ProjectID = tasks.ProjectID,
                                SpecializationID = programmers.SpecializationID,
                                Charge = tasks.Charge
                          };
            }
            return list;
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
            if (task.ProjectID == 0)
                task.ProjectID = context.Projects.First().ProjectID;
            var project = context.Projects.Find(task.ProjectID);
            task.Color = project.Color;
            task.Title = project.Name;

            var entity = task.ToEntity();
            entity.Project = context.Projects.Find(task.ProjectID);
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
                original.Charge = entityTask.Charge;
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