using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public partial class ProcessConnection
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int ConnectionId { get; set; }
    }
}
