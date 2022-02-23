using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public partial class PersonDocument
    {
        public int Id { get; set; }
        public int? DocumentTypeId { get; set; }
        public string Path { get; set; }
        public string Remarks { get; set; }
        public decimal? PersonId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Person Person { get; set; }
    }
}
