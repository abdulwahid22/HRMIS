using NHCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Application.ProcessTracks.Models
{
    public class SearchedProcessTracks
    {

        public int Id { get; set; }
        public int RecordId { get; set; }
        public short ProcessId { get; set; }
        public short ReferedProcessId { get; set; }
        public short StatusId { get; set; }
        public string StatusText { get; set; }
        public string Remarks { get; set; }
        public int ModuleId { get; set; }
        public string ProcessText { get; set; }
        public string ModuleText { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DateText { get; set; }
    }



}
