using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonSkill
    {
        public decimal? Id { get; set; }
        public decimal PersonId { get; set; }
        public int? LanguageId { get; set; }
        public short ExpertiseId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? Locationid { get; set; }
        public string Remarks { get; set; }
        public int? CertificationId { get; set; }
        public string CertifiedFrom { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 

        public string LanguageText { get; set; }
        
        public string ExpertiseText { get; set; }
        public string CertificationText { get; set; }


        public String StartDateText { get; set; }
        public String EndDateText { get; set; }

    }
}
