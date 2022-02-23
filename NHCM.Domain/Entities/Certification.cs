using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
   public class Certification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SkillTypeId { get; set; }

        public virtual SkillType SkillType { get; set; }
    }
}
