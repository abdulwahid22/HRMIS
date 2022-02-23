using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class ReferenceType
    {
        public ReferenceType()
        {
            Reference = new HashSet<Reference>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reference> Reference { get; set; }
    }
}
