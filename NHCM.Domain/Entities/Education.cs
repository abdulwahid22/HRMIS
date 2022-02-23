using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Education
    {
        public decimal Id { get; set; }
        public decimal? PersonId { get; set; }
        public short EducationLevelId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OfficialDocumentNo { get; set; }
        public int? LocationId { get; set; }
        public string Institute { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public bool? Inservice { get; set; }
        public string Major { get; set; }
        public string Remarks { get; set; }
        public string MigratedLocation { get; set; }
        public string Faculty { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }
    }
}
