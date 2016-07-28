using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class Department
    {
        public Department()
        {
            Programmers = new List<Programmer>();
        }

        public int DepartmentID { get; set; }

        public string Name { get; set; }

        [Required]
        public virtual List<Programmer> Programmers { get; set; }
    }
}