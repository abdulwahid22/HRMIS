using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.ViewsEntities
{
 public  class ZoneCardDetails
    {
        public int ID { get; set; }
        public string Hrcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string position { get; set; }
        public string Title { get; set; }
        public string department { get; set; }
        public string zone { get; set; }
        public int ZoneID { get; set; }
        public int numofemployee { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
