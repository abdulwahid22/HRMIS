using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Ethnicity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? CountryId { get; set; }
    }
}
