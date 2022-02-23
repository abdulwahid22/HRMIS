using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.ViewsEntities
{
    public class carddetails
    {
        public string hrcode { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
        public string name1 { get; set; }

        public DateTime ExpiryDate { get; set; }
        public int  gift { get; set; }
    }
}

 