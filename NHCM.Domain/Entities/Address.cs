using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Address
    {

        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? AddressTypeId { get; set; }
        public int? LocationId { get; set; }
        public int? DistrictId { get; set; }
        public string Village { get; set; }
        public string Address1 { get; set; }
        public string StreetNo { get; set; }
        public string HouseNo { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public bool? IsActive { get; set; }
        public string Paddress { get; set; }
        public int? ClocationId { get; set; }
        public int? CdistrictId { get; set; }
        public string Cvillage { get; set; }
    }
}
