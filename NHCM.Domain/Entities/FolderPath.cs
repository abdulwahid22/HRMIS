using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Domain.Entities
{
    public class FolderPath
    {
        public int Id { get; set; }
        public string SettingsKey { get; set; }
        public string CurrentFolder { get; set; }
    }
}
