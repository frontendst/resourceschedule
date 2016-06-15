using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResourceManagementSystem2.Models
{
    public class Specialization
    {
        public Specialization()
        {
            Programmers = new List<Programmer>();
        }

        public int SpecializationID { get; set; }

        public string Name { get; set; }

        [Required]
        public virtual List<Programmer> Programmers { get; set; }

    }
}