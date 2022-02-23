using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class PlanType
    {

        public short Id { get; set; }
        public string Name { get; set; }
        public char Code { get; set; }
        public short? ParentId { get; set; }
    }
}
