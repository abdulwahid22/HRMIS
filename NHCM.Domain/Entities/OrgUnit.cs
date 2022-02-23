using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public  class OrgUnit
    {
        public OrgUnit()
        {
           // Employee = new HashSet<Employee>();
            InverseParent = new HashSet<OrgUnit>();
          //  OrgUnitChangeNeworgUnit = new HashSet<OrgUnitChange>();
          //  OrgUnitChangeOrgUnit = new HashSet<OrgUnitChange>();
           // Selection = new HashSet<Selection>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Modifiedby { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public decimal? ParentId { get; set; }
        public int? LocationId { get; set; }
        public string Code { get; set; }
        public int? StatusId { get; set; }
        public short? OrgunitTypeId { get; set; }
        public string Sorter { get; set; }
        public int? OrganOgramId { get; set; }

        public virtual OrganoGram OrganOgram { get; set; }
        public virtual OrgUnit Parent { get; set; }
      //  public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<OrgUnit> InverseParent { get; set; }
      //  public virtual ICollection<OrgUnitChange> OrgUnitChangeNeworgUnit { get; set; }
      //  public virtual ICollection<OrgUnitChange> OrgUnitChangeOrgUnit { get; set; }
     //   public virtual ICollection<Selection> Selection { get; set; }
    }
}
