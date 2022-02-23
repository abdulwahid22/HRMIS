using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonEducations
    {


        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short EducationLevelId { get; set; }
        public string EducationLevelText { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public String StartDateText { get; set; }
        public String EndDateText { get; set; }
        public int? LocationId { get; set; }
        public string LocationText { get; set; }
        public string Institute { get; set; }
        public string Faculty { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public string OfficialDocumentNo { get; set; }
        public string Major { get; set; }
        public string Remarks { get; set; }


        public bool? Inservice { get; set; }
               
        public string MigratedLocation { get; set; }
        






    }
}
