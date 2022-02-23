using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
  public   class SearchedPersonPublication
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short PublicationTypeId { get; set; }
        public string Subject { get; set; }
        public DateTime PublishDate { get; set; }
        public string ReferenceNo { get; set; }
        public string Isbn { get; set; }
        public int? NoofPages { get; set; }

        public String PublishDateText { get; set; }
        public string PublicationTypeText { get; set; }
    }
}
