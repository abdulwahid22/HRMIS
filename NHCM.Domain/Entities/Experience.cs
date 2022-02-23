using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Experience
    {

        public string Designation { get; set; }
        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public string Organization { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
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
    }
}
