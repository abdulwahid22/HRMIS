using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Institute
    {

        public Institute()
        {
            InverseParent = new HashSet<Institute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? LocationId { get; set; }

        public virtual Institute Parent { get; set; }
        public virtual ICollection<Institute> InverseParent { get; set; }
    }
}
