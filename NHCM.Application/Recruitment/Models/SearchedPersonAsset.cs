using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Recruitment.Models
{
    public partial class SearchedPersonAsset
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short? AssetTypeId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }


        public string AssetTypeText { get; set; }
    }
}
