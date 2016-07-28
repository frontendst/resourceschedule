using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class WeekendViewModel
    {
        public WeekendViewModel() { }

        public int WeekendViewModelID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public string Description { get; set; }

        public WeekendViewModel(Weekend entity)
        {
            this.WeekendViewModelID = entity.WeekendID;
            this.Date = entity.Date;
            this.Description = entity.Description;
        }

        public Weekend ToEntity()
        {
            return new Weekend
            {
                WeekendID = this.WeekendViewModelID,
                Date = this.Date,
                Description = this.Description
            };
        }
    }
}