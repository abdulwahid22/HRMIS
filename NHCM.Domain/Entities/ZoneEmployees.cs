using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class ZoneEmployees
    {
        public int ID { get; set; }
        public decimal? PersonID { get; set; }
        public decimal? ZoneID { get; set; }
        public int Code { get; set; }
    }
}
