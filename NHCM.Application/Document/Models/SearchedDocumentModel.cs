using System;
using System.Collections.Generic;

namespace NHCM.Application.Document.Models
{
    public partial class SearchedDocumentModel
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Module { get; set; }
        public string Item { get; set; }
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
        public int? DocumentTypeId { get; set; }
        public DateTime? LastDownloadDate { get; set; }
        public String DocumentTypeText { get; set; }
        public String DownloadDateText { get; set; }
        public String UploadDateText { get; set; }
    }
}
