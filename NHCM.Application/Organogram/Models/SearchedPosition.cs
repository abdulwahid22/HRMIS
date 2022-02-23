
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Organogram.Models
{
    public class SearchedPosition
    {

        public decimal Id { get; set; }
        public int WorkingAreaId { get; set; }
        public int NodeId { get; set; }
        public int ParentNodeId { get; set; }
        public decimal? ParentId { get; set; }
        public int? PositionTypeId { get; set; }
        public int? StatusId { get; set; }
        public int? LocationId { get; set; }
        public int? SalaryTypeId { get; set; }
        public string Sorter { get; set; }
        public int? OrganoGramId { get; set; }
        public short? PlanTypeId { get; set; }
        public string Code { get; set; }
        public string LocationText { get; set; }
        public string SalaryTypeText { get; set; }
        public string PlanTypeText { get; set; }
        public string WorkAreaText { get; set; }
        public string RankText { get; set; }
        public string PositionTypeText { get; set; }
        public string OrgUnitText { get; set; }
        //public int? Year { get; set; }

    }
}
