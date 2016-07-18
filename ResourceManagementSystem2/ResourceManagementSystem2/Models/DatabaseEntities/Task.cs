using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{ 
    public class Task
    {
        public int TaskID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public float Charge { get; set; }

        public int ProgrammerID { get; set; }
        public Programmer Programmer { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }

    }
}