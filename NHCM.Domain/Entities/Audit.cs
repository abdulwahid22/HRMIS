using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
  public  class Audit
    {
        public long Id { get; set; }
        public string DbContextObject { get; set; }
        public string DbOjbectName { get; set; }
        public string ReocordId { get; set; }
        public int? OperationTypeId { get; set; }
        public int UserId { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
        public DateTime? OperationDate { get; set; }

        public virtual OperationType OperationType { get; set; }
    }
}
