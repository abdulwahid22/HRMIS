using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public short OrgUnitTypeId { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public int Reference { get; set; }
    }
}
