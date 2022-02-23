using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public  class Publication
    {
        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public short PublicationTypeId { get; set; }
        public string Subject { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Isbn { get; set; }
        public int? NoofPages { get; set; }

        public virtual Person Person { get; set; }
        public virtual PublicationType PublicationType { get; set; }
    }
}
