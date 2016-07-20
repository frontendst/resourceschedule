using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceManagementSystem2.Models
{
    public class ProgrammerService
    {
        private readonly DbContext context;

        public ProgrammerService(DbContext context)
        {
            this.context = context;
        }

        public ProgrammerService()
        {
            context = new DbContext();
        }

        public IQueryable<ProgrammerViewModel> GetAll(int page = 0, int pageSize = 0)
        {
            List<Programmer> programmerList = null;

            if (pageSize == 0)
            {
                programmerList = context.Programmers.ToList();
            }
            else
            {
                programmerList = context.Programmers.OrderBy(p=>p.ProgrammerID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            var programmerViewList = new List<ProgrammerViewModel>();
            foreach (var p in programmerList)
            {
                programmerViewList.Add(new ProgrammerViewModel(p));
            }
            return programmerViewList.AsQueryable();
        }

        public int Count()
        {
            return context.Programmers.Count();
        }

        public IQueryable<Programmer> GetProgramerEntities()
        {
            return context.Programmers.AsQueryable();
        }

        public void Insert(ProgrammerViewModel programmer)
        {
            var programmerEntity = programmer.ToEntity();
            programmerEntity.SpecializationID = programmerEntity.Specialization.SpecializationID;
            programmerEntity.Specialization = null;
            programmerEntity.DepartmentID = programmerEntity.Department.DepartmentID;
            programmerEntity.Department = null;
            programmerEntity = context.Programmers.Add(programmerEntity);
            context.SaveChanges();
            programmer.ProgrammerViewModelID = programmerEntity.ProgrammerID;
        }

        public void Delete(ProgrammerViewModel programmer)
        {
            context.Programmers.Remove(context.Programmers.Find(programmer.ProgrammerViewModelID));
            context.SaveChanges();
        }

        public void Update(ProgrammerViewModel programmer)
        {
            var original = context.Programmers.Find(programmer.ToEntity().ProgrammerID);
            var entityProgrammer = programmer.ToEntity();

            if (original != null)
            {
                original.Name = entityProgrammer.Name;
                original.SpecializationID = entityProgrammer.Specialization.SpecializationID;
                original.DepartmentID = entityProgrammer.Department.DepartmentID;
            }

            context.SaveChanges();
        }
    }
}