using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ResourceManagementSystem2.Models
{
    public class Project
    {
        public Project()
        {
            Tasks = new List<Task>();
        }

        public int ProjectID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        [Required]
        public virtual List<Task> Tasks { get; set; }
    }
}