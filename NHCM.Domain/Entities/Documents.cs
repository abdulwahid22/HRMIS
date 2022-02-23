using System;
using System.Collections.Generic;

namespace NHCM.Domain.Entities
{

    public  class Documents

    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string ObjectSchema { get; set; }
        public string ObjectName { get; set; }
        public string RecordId { get; set; }
        public string Root { get; set; }
        public string Path { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string EncryptionKey { get; set; }
        public string ReferenceNo { get; set; }
        public int? StatusId { get; set; }
        public string Description { get; set; }
        public int? ScreenId { get; set; }
        public DateTime? LastDownloadDate { get; set; }

    }
}
