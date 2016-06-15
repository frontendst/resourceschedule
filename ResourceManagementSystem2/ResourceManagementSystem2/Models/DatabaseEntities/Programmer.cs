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
            Specializations = new List<Specialization>();
            Projects = new List<Project>();
        }

        public int ProgrammerID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public virtual List<Specialization> Specializations { get; set; }

        [Required]
        public virtual List<Project> Projects { get; set; }
    }
}