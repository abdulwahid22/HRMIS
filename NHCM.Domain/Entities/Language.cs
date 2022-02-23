using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Language
    {
        public Language()
        {
            PersonLanguage = new HashSet<PersonLanguage>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public virtual ICollection<PersonLanguage> PersonLanguage { get; set; }
    }
}