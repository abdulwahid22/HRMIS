using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Auditversion
    {

        public int Id { get; set; }
        public long? AuditId { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }

        public virtual Audit Audit { get; set; }
    }
}
