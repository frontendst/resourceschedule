using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class SpecializationViewModel : IComparable
    {
        public SpecializationViewModel() { }

        public int SpecializationViewModelID { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> Programmers { get; set; }

        public SpecializationViewModel(Specialization specialization)
        {
            if (specialization != null)
            {
                SpecializationViewModelID = specialization.SpecializationID;
                Name = specialization.Name;
                //Programmers = specialization.Programmers.Select(x => x.ProgrammerID);
            }
        }

        public Specialization ToEntity()
        {
            var specialization = new Specialization
            {
                SpecializationID = SpecializationViewModelID,
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

            specialization.Programmers = resultProgrammerList;

            return specialization;
        }

        public int CompareTo(object obj)
        {
            var specStr = ((SpecializationViewModel)obj).Name;
            return string.Compare(Name, specStr);
        }
    }
}