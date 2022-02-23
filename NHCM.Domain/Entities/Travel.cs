using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
   public  class Travel
    {
        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public int CountryId { get; set; }
        public string Place { get; set; }
        public DateTime? TravelDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Reason { get; set; }
        public string AccompanyWith { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Person Person { get; set; }
    }
}
