using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
   public  class Reference
    {

        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Occupation { get; set; }
        public string Organization { get; set; }
        public string TelephoneNo { get; set; }
        public string District { get; set; }
        public int? LocationId { get; set; }
        public string RelationShip { get; set; }
        public short? ReferenceTypeId { get; set; }
        public string Amount { get; set; }
        public int? BankId { get; set; }
        public string ReceiptNumber { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string Remark { get; set; }

       // public virtual Bank Bank { get; set; }
        public virtual ReferenceType ReferenceType { get; set; }
    }
}
