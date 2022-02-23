using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Organogram.Models
{
    public class SearchedOrgPosition
    {
        public int PositionTypeId { get; set; }
        public int OrgUnitTypeId { get; set; }
        public int? ParentId { get; set; }
        public int Id { get; set; }
        public short RankId { get; set; }

        public string RankText { get; set; }
        public string PositionTypeText { get; set; }
        public string OrgUnitText { get; set; }
    }


}
