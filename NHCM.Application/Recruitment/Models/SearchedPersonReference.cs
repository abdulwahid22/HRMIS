﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonReference
    {


        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
       
        public string ReferenceNo { get; set; }
       
       
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
        public string Remark { get; set; }

        public string ReferenceTypeText { get; set; }
        public string LocationText { get; set; }

         
    }
}
