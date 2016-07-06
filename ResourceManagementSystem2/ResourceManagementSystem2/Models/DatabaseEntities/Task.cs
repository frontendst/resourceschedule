﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{ 
    public class Task
    {
        public int TaskID { get; set; }

        public string Text { get; set; }
        //[Index]
        public DateTime StartTime { get; set; }
        //[Index]
        public DateTime EndTime { get; set; }

        public int ProgrammerID { get; set; }
        public Programmer Programmer { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }

    }
}