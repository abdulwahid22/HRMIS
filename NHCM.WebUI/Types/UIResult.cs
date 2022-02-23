using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHCM.WebUI.Types
{
    public class UIResult
    {

        public int? Status { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public object Data { get; set; }
    }
}
