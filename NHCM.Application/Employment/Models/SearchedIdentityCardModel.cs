using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Employment.Models
{
   public class SearchedIdentityCardModel
    {
        public long Id { get; set; }
        public string CardCode { get; set; }
        public decimal? PersonId { get; set; }
        public string ExpiryDate { get; set; }
        public string IssueDate { get; set; }
        public string PhotoPath { get; set; }
         public string CardPrinted { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }

    }
}
