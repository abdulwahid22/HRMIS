using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public class SearchedPersonTravel
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public int? CountryId { get; set; }
        public string Place { get; set; }
        public DateTime? TravelDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Reason { get; set; }
        public string AccompanyWith { get; set; }
        public string ReferenceNo { get; set; }
        
        public string CountryText { get; set; }


        public String TravelDateText { get; set; }
        public String ReturnDateText { get; set; }
    }
}
