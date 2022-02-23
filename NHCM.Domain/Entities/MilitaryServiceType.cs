using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class MilitaryServiceType
    {
        public MilitaryServiceType()
        {
            MilitaryService = new HashSet<MilitaryService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MilitaryService> MilitaryService { get; set; }
    }
}
