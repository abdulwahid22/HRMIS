using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class EducationLevel
    {
        public EducationLevel()
        {
            Education = new HashSet<Education>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public short? Parentid { get; set; }
        public string Sorter { get; set; }

        public virtual ICollection<Education> Education { get; set; }
    }
}
