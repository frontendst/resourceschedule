using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace ResourceManagementSystem2.Models
{
    public class Programmer
    {
        public Programmer()
        {
            Tasks = new List<Task>();
        }

        public int ProgrammerID { get; set; }

        public string Name { get; set; }

        public int? SpecializationID { get; set; }
        public virtual Specialization Specialization { get; set; }

        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public virtual List<Task> Tasks { get; set; }
    }
}