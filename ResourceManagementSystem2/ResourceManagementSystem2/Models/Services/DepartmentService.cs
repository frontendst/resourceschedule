using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class DepartmentService
    {
        private readonly DbContext context;

        public DepartmentService(DbContext context)
        {
            this.context = context;
        }

        public DepartmentService()
        {
            context = new DbContext();
        }

        public IQueryable<DepartmentViewModel> GetAll()
        {
            var departmentList = context.Departments.ToList();
            var depViewList = new List<DepartmentViewModel>();
            foreach (var d in departmentList)
            {
                depViewList.Add(new DepartmentViewModel(d));
            }
            return depViewList.AsQueryable();
        }

        public void Insert(DepartmentViewModel department)
        {

            var departmentEntity = context.Departments.Add(department.ToEntity());
            context.SaveChanges();
            department.DepartmentViewModelID = departmentEntity.DepartmentID;
        }

        public bool Delete(DepartmentViewModel department)
        {
            if (department.Programmers != null && department.Programmers.First() != 0)
                return false;
            context.Departments.Remove(context.Departments.Find(department.DepartmentViewModelID));
            try
            {
                context.SaveChanges(); // при попытке удаления непустых специализаци и отделов
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public void Update(DepartmentViewModel department)
        {
            var original = context.Departments.Find(department.ToEntity().DepartmentID);
            var entityDep = department.ToEntity();
            if (original != null)
            {
                original.Name = entityDep.Name;
            }
            context.SaveChanges();
        }
    }
}