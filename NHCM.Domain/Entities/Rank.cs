using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
   public class Rank
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string Sorter { get; set; }
        public short? CategoryId { get; set; }
        public string RankNumber { get; set; }
    }
}
