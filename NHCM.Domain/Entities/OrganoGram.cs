using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class OrganoGram
    {
        public OrganoGram()
        {
            OrgUnit = new HashSet<OrgUnit>();
           // OrgUnitChange = new HashSet<OrgUnitChange>();
            PositionChange = new HashSet<PositionChange>();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int StatusId { get; set; }
        public int Year { get; set; }
        public int IsPositionsCopied { get; set; }
        public int NumberOfPositions { get; set; }
        public virtual ICollection<OrgUnit> OrgUnit { get; set; }
       // public virtual ICollection<OrgUnitChange> OrgUnitChange { get; set; }
        public virtual ICollection<PositionChange> PositionChange { get; set; }
    }
}
