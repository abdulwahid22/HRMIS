using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class Person
    {
        public Person()
        {
            HealthReport = new HashSet<HealthReport>();
          //  IdCard = new HashSet<IdCard>();
            MilitaryService = new HashSet<MilitaryService>();
            PersonAsset = new HashSet<PersonAsset>();
           // PersonCharacteristic = new HashSet<PersonCharacteristic>();
            PersonDocument = new HashSet<PersonDocument>();
            PersonLanguage = new HashSet<PersonLanguage>();
            PersonSkill = new HashSet<PersonSkill>();
         //   Promotion = new HashSet<Promotion>();
            Publication = new HashSet<Publication>();
            Travel = new HashSet<Travel>();
        }

        public string FatherName { get; set; }
        public decimal Id { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string Hrcode { get; set; }
        public string LastName { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string HashKey { get; set; }
        public string PreFix { get; set; }
        public int? Suffix { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public string GrandFatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public short? MaritalStatusId { get; set; }
        public int? EthnicityId { get; set; }
        public short? ReligionId { get; set; }
        public string Comments { get; set; }
        public int? StatusId { get; set; }
        public string Remark { get; set; }
        public int? BloodGroupId { get; set; }
        public string Hiringofficenumber { get; set; }
        public string Nid { get; set; }
        public string PhotoPath { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? OrganizationId { get; set; }


        public virtual Organization Organization { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<HealthReport> HealthReport { get; set; }
       // public virtual ICollection<IdCard> IdCard { get; set; }
        public virtual ICollection<MilitaryService> MilitaryService { get; set; }
        public virtual ICollection<PersonAsset> PersonAsset { get; set; }
        //public virtual ICollection<PersonCharacteristic> PersonCharacteristic { get; set; }
        public virtual ICollection<PersonDocument> PersonDocument { get; set; }
        public virtual ICollection<PersonLanguage> PersonLanguage { get; set; }
        public virtual ICollection<PersonSkill> PersonSkill { get; set; }
        //public virtual ICollection<Promotion> Promotion { get; set; }
        public virtual ICollection<Publication> Publication { get; set; }
        public virtual ICollection<Travel> Travel { get; set; }
    }
}

