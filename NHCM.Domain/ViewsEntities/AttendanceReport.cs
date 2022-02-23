using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.ViewsEntities
{
    public class AttendanceReport
    {
        public int userid { get; set; }
        public string FullName { get; set; } 
        public string FatherName { get; set; }
        public string postion { get; set; }

        public int departmentid { get; set; }
        public string department { get; set; }
        public string bast { get; set; }
        public string qadam { get; set; }
        public int absent { get; set; }
        public int present { get; set; }
        public int leave { get; set; }

        public String AttDate { get; set; }
        public String AbsentDates { get; set; }
    }
}
 