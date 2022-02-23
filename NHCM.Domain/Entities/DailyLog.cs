using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class DailyLog
    {
        public decimal Id { get; set; }
        public int UserId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
