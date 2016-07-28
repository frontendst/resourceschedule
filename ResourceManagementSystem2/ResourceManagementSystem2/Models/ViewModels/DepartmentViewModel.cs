using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel() { }

        public int DepartmentViewModelID { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> Programmers { get; set; }

        public DepartmentViewModel(Department department)
        {
            if (department != null)
            {
                DepartmentViewModelID = department.DepartmentID;
                Name = department.Name;
            }
        }

        public Department ToEntity()
        {
            var department = new Department
            {
                DepartmentID = DepartmentViewModelID,
                Name = this.Name
            };

            var resultProgrammerList = new List<Programmer>();

            using (var context = new DbContext())
            {
                var programmers = context.Programmers;
                if (Programmers != null)
                {
                    foreach (var num in Programmers)
                    {
                        resultProgrammerList.Add(programmers.Find(num));
                    }
                }
            }

            department.Programmers = resultProgrammerList;

            return department;
        }

        public int CompareTo(object obj)
        {
            var depStr = ((SpecializationViewModel)obj).Name;
            return string.Compare(Name, depStr);
        }
    }
}