using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class ProgrammerViewModel
    {
        public ProgrammerViewModel() { }

        public int ProgrammerViewModelID { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> Tasks { get; set; }

        public int? SpecializationID { get; set; }

        public SpecializationViewModel Specialization { get; set; }

        public ProgrammerViewModel(Programmer programmer)
        {
            ProgrammerViewModelID = programmer.ProgrammerID;
            Name = programmer.Name;
            Tasks = null;// programmer.Tasks.Select(t => t.TaskID);
            SpecializationID = programmer.SpecializationID;
            Specialization = new SpecializationViewModel(programmer.Specialization);
        }

        public Programmer ToEntity()
        {
            var programmer =  new Programmer { Name = this.Name };

            programmer.ProgrammerID = ProgrammerViewModelID;
            programmer.SpecializationID = SpecializationID; 
            var resultTasksList = new List<Task>();
            using (var context = new DbContext())
            {
                var tasks = context.Tasks;
                if (Tasks != null)
                {
                    foreach (var num in Tasks)
                    {
                        resultTasksList.Add(tasks.Find(num));
                    }
                }
                programmer.Tasks = resultTasksList;             
                programmer.Specialization = Specialization?.ToEntity();
            }
            return programmer;
        }
    }
}