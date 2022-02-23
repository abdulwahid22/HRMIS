using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
   public  class SearchedPersonMilitaryService
    {


        public int Id { get; set; }
        public decimal PersonId { get; set; }
        public int MilitaryServiceTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remark { get; set; }


        public String StartDateText { get; set; }
        public String EndDateText { get; set; }
        public string MilitaryServiceTypeText { get; set; }
    }
}
