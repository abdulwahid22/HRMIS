using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.Reports.Models
{
    public class MonthlyRecord
    {
        public int DeptID { get; set; }
        public DateTime Date { get; set; }
        public String NameLocal { get; set; }
        public String FatherNameLocal { get; set; }
        public String Bast { get; set; }
        public String Qadam { get; set; }
        public String UserID { get; set; }
        public String Department { get; set; }
        public String TITLE { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Leave { get; set; }
        public String AttDate { get; set; }
        public String AbsentDates { get; set; }
    }
}
