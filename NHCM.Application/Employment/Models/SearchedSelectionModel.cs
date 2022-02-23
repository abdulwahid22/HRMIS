using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Employment.Models
{
    public class SearchedSelectionModel
    {
        public decimal SelectionId { get; set; }
        public decimal PositionId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal Id { get; set; }
        public int EventTypeId { get; set; }
        public decimal PersonId { get; set; }
        public short? CategoryId { get; set; }
        public decimal? EventId { get; set; }
        public string Remarks { get; set; }
        public DateTime? VerdictDate { get; set; }
        public string VerdictRegNo { get; set; }
        public string FinalNo { get; set; }
        public string ReferenceNo { get; set; } 
        public short? QadamID { get; set; }
        public int DepartmentID { get; set; }
        // TEMP:
        public string DateText { get; set; }
        public int OrganogramId { get; set; }
        public int ParentId { get; set; }

        public string WorkAreaText { get; set; }
        public string RankText { get; set; }
        public string PositionTypeText { get; set; }
        public string OrgUnitText { get; set; }
        public string Sorter { get; set; }
        public string Code { get; set; }
        public string LocationText { get; set; }

        public string Title { get; set; }
        public string PersonName { get; set; }
        public int NodeId { get; set; }
        public int ParentNodeId { get; set; }

    }
}
