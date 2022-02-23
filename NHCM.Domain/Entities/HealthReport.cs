using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
   public  class HealthReport
    {

        public decimal Id { get; set; }
        public decimal? PersonId { get; set; }
        public DateTime ReportDate { get; set; }
        public int StatusId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public bool? Approved { get; set; }
        public string Remarks { get; set; }

        public virtual Person Person { get; set; }
    }
}
