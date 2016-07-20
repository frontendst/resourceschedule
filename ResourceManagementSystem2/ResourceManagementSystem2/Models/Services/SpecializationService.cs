using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class SpecializationService
    {
        private readonly DbContext context;

        public SpecializationService(DbContext context)
        {
            this.context = context;
        }

        public SpecializationService()
        {
            context = new DbContext();
        }

        public IQueryable<SpecializationViewModel> GetAll()
        {
            var specializationList = context.Specializations.ToList();
            var specViewList = new List<SpecializationViewModel>();
            foreach (var p in specializationList)
            {
                specViewList.Add(new SpecializationViewModel(p));
            }
            return specViewList.AsQueryable();
        }

        public void Insert(SpecializationViewModel specialization)
        {

            var specializationEntity = context.Specializations.Add(specialization.ToEntity());
            context.SaveChanges();
            specialization.SpecializationViewModelID = specializationEntity.SpecializationID;
        }

        public bool Delete(SpecializationViewModel specialization)
        {
            if (specialization.Programmers != null && specialization.Programmers.First() != 0)
                return false;
            context.Specializations.Remove(context.Specializations.Find(specialization.SpecializationViewModelID));
            context.SaveChanges();
            return true;
        }

        public void Update(SpecializationViewModel specialization)
        {
            var original = context.Specializations.Find(specialization.ToEntity().SpecializationID);
            var entitySpec = specialization.ToEntity();
            if (original != null)
            {
                original.Name = entitySpec.Name;
            }
            context.SaveChanges();
        }

    }
}