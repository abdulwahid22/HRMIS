using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public  class MilitaryService
    {
        public int Id { get; set; }
        public decimal PersonId { get; set; }
        public int MilitaryServiceTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }

        public virtual MilitaryServiceType MilitaryServiceType { get; set; }
        public virtual Person Person { get; set; }
    }
}
