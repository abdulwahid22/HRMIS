using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Organogram.Models
{
    public class SearchedOrganogramControll
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int StatusId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int? Year { get; set; }
        public DateTime? PreparedDate { get; set; }
        public DateTime? AppreovedDate { get; set; }
        public int? NumberOfPositions { get; set; }

        public string statustext { get; set; }
        public string TitleText { get; set; }
        public string OrganizationText { get; set; }
    }
}
