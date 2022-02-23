using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Employment.Models
{
    public class SearchedZoneModel
    {
        public int? ID { get; set; }
        public decimal? PersonID { get; set; }
        public decimal? ZoneID { get; set; }
        public int? Code { get; set; }
        public string FirstName { get; set; }
        public string ZoneName { get; set; }
      

    }
}
