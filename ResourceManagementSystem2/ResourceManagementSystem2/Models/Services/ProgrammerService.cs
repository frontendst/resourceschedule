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

        public IQueryable<ProgrammerViewModel> GetAll()
        {
            var programmerList = context.Programmers.ToList();
            var programmerViewList = new List<ProgrammerViewModel>();
            foreach (var p in programmerList)
            {
                programmerViewList.Add(new ProgrammerViewModel(p));
            }

            var debug = programmerViewList.AsQueryable();
            return programmerViewList.AsQueryable();
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
            }

            context.SaveChanges();
        }
    }
}