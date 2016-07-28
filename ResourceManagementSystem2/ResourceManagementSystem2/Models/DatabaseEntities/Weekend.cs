using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class Weekend
    {
        public int WeekendID { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }
    }
}