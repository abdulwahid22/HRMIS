using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonRelative
    {
        public string FatherName { get; set; }
        public decimal? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? PersonId { get; set; }
        public string GrandFatherName { get; set; }
        public int? RelationShipId { get; set; }
        public string NidNo { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string EmailAddress { get; set; }
        public string Village { get; set; }
        public int? LocationId { get; set; }
        public string Remark { get; set; }

        public string RelationShipIdText { get; set; }
        public string LocationText { get; set; }


      
       
    }
}
