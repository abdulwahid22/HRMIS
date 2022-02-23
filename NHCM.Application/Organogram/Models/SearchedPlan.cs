using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Organogram.Models
{
   
    public class SearchedPlan
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int StatusId { get; set; }
        public int Year { get; set; }
        public int IsPositionsCopied { get; set; }
        public int NumberOfPositions { get; set; }
        public string StatusText { get; set; }
        public string OrganizationText { get; set; }
        public string CreationType { get; set; }
    }
}
