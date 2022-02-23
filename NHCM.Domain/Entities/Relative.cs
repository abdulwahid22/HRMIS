using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Relative
    {


        // CHANGE: Delete extra columns both from database and this class. There are a bunch of null columns in the database

        public string FatherName { get; set; }
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? PersonId { get; set; }
        public string GrandFatherName { get; set; }
        public int? RelationShipId { get; set; }
        public string NidNo { get; set; }
        public string Profession { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public string PersonalProperty { get; set; }
        public string EmailAddress { get; set; }
        public string CurrentVillage { get; set; }
        public string Village { get; set; }
        public int? CurrentLocationId { get; set; }
        public int? LocationId { get; set; }
        public short? ReligionId { get; set; }
        public string JobLocation { get; set; }
        public string Remark { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Religion Religion { get; set; }
    }
}
