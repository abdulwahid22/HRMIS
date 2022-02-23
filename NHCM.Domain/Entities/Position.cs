using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{
    public partial class Position
    {
        public Position()
        {
          //  Employee = new HashSet<Employee>();
            PositionChange = new HashSet<PositionChange>();
          //  PositionResponsibility = new HashSet<PositionResponsibility>();
            Selection = new HashSet<Selection>();
        }

        public decimal Id { get; set; }
        public int WorkingAreaId { get; set; }
        public int? Reference { get; set; }
        public decimal? ParentId { get; set; }
        public string Sorter { get; set; }
        public int PositionTypeId { get; set; }
        public int StatusId { get; set; }
        public int LocationId { get; set; }
        public int SalaryTypeId { get; set; }
        //public string Sorter { get; set; }
        public int OrganoGramId { get; set; }
        public short PlanTypeId { get; set; }
        public string Code { get; set; }

      //  public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<PositionChange> PositionChange { get; set; }
       // public virtual ICollection<PositionResponsibility> PositionResponsibility { get; set; }
        public virtual ICollection<Selection> Selection { get; set; }
    }
}
