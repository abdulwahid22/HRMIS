using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHCM.WebUI.Types
{
    public class UIStatus
    {
        public static byte Success { get; } = 1;
        public static byte Failure { get; } = 0;
        public static byte SuccessWithoutMessage { get; } = 2;
    }
}
