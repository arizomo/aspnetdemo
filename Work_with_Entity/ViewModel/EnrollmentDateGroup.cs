using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work_with_Entity.ViewModel
{
    public class EnrollmentDateGroup
    {
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}