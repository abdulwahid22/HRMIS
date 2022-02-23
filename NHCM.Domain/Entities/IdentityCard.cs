using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
   public class IdentityCard
    {
        public long Id { get; set; }
        public decimal? PersonId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CardClassType { get; set; }
        public string CardCode { get; set; }
        
        public string PhotoPath { get; set; }
        public int? StatusID { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
