using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonHealthReport
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public DateTime ReportDate { get; set; }
        public int? StatusId { get; set; } 
        public string ReferenceNo { get; set; }  
        public bool? Approved { get; set; }
        public string Remarks { get; set; }  
        public string StatusText { get; set; }


        public String ReportDateText { get; set; } 
    }
}
