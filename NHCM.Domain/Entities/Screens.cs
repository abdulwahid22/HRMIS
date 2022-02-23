using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Screens
    {
        public Screens()
        {
            DocumentType = new HashSet<DocumentType>();
            InverseParent = new HashSet<Screens>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public short? Sorter { get; set; }
        public int? ParentId { get; set; }
        public int? ModuleId { get; set; }
        public virtual Screens Parent { get; set; }
        public virtual ICollection<DocumentType> DocumentType { get; set; }
        public virtual ICollection<Screens> InverseParent { get; set; }
    }

}
