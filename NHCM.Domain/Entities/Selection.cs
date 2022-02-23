using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public  class Selection
    {
        public decimal PositionId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal Id { get; set; }
        public int EventTypeId { get; set; }
        public decimal PersonId { get; set; }
        public string Remarks { get; set; }
        public DateTime? VerdictDate { get; set; }
        public string VerdictRegNo { get; set; }
        public string FinalNo { get; set; }
        public short? QadamID { get; set; }
        public string ReferenceNo { get; set; }
        public int DepartmentID { get; set; }

        public virtual Position Position { get; set; }

    }
}
