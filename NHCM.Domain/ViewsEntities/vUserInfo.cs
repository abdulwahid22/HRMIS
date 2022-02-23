using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.ViewsEntities
{
    public class vUserInfo
    {
        public int userid { get; set; }
        
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public int departmentid { get; set; }
        public string DepartmentText { get; set; }
        public string Title { get; set; }
         
        public string bast { get; set; }
        public string qadam { get; set; }
        
    }
}
