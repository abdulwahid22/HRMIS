using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public partial class PersonAsset
    {
        public decimal Id { get; set; }
        public decimal PersonId { get; set; }
        public short AssetTypeId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }

        public virtual AssetType AssetType { get; set; }
        public virtual Person Person { get; set; }
    }
}
