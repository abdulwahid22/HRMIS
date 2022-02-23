using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Lookup.Models
{
    public class SearchedOrgPositionModel
    {
        public int Id { get; set; }
        public int PositionTypeId { get; set; }
        public int OrgUnitTypeId { get; set; }
        public int? ParentId { get; set; }
        public short RankId { get; set; }

        public string PositionTypeText { get; set; }
        public string OrgUnitTypeText { get; set; } 
        public string ParentText { get; set; }  
        public string RankText { get; set; }
    }
}
