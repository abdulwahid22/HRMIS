using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonExperience
    {
        public string Designation { get; set; }
        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public string Organization { get; set; }
        public short? RequestNo { get; set; }
        public int? LocationId { get; set; }
        public string DocumentNo { get; set; }
        public short? RankId { get; set; }
        public short? PromotionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContactInfo { get; set; }
        public int? JobstatusId { get; set; }
        public string JobDescription { get; set; }
        public bool? Approved { get; set; }
        public string Remarks { get; set; }
        public int? ExperienceTypeId { get; set; }


        public string LocationText { get; set; }
        public string RankText { get; set; }
        public string PromotionText { get; set; }
        public string JobStatusText { get; set; }
        public string ExperienceTypeText { get; set; } 
        public string Duration { get; set; }

        public String StartDateText { get; set; }
        public String EndDateText { get; set; }

    }
}
