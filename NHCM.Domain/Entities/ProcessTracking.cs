using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public partial class ProcessTracking
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public short ProcessId { get; set; }
        public short ReferedProcessId { get; set; }
        public short StatusId { get; set; }
        public string Remarks { get; set; }
        public int ModuleId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
