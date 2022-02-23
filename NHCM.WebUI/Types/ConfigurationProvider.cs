using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHCM.WebUI.Types
{
    public static class ConfigurationProvider 
    {
        public static string _ShowStackTrace;

        public static IServiceProvider SetConfigurationProvider(this IServiceProvider serviceProvider, IConfiguration configurationRoot)
        {
            _ShowStackTrace = configurationRoot.GetValue<string>("ShowStackTrace");
            return serviceProvider;
        }
    }
}
