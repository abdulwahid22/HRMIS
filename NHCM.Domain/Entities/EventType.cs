using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public partial class EventType
    {
        public EventType()
        {
            InverseParent = new HashSet<EventType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual EventType Parent { get; set; }
        public virtual ICollection<EventType> InverseParent { get; set; }
    }
}
