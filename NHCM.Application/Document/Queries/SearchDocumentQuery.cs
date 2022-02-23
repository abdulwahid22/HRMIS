using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NHCM.Application.Recruitment.Models;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using NHCM.Persistence;
using NHCM.Application.Common;
using NHCM.Application.Document.Models;

namespace NHCM.Application.Document.Queries
{
    public class SearchDocumentQuery : IRequest<List<SearchedDocumentModel>>
    {

        public int? Id { get; set; }
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


    public class SearchDocumentQueryHandler : IRequestHandler<SearchDocumentQuery, List<SearchedDocumentModel>>
    {

        private readonly HCMContext _context;
        //private readonly IMediator _mediator;

        public SearchDocumentQueryHandler(HCMContext context/*, IMediator mediator*/)
        {
            _context = context;
            //_mediator = mediator;
        }

        public async Task<List<SearchedDocumentModel>> Handle(SearchDocumentQuery request, CancellationToken cancellationToken)
        {
            List<SearchedDocumentModel> result = new List<SearchedDocumentModel>();
            if (request.Id != null)
            {
                result = await (from d in _context.Document
                                //join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id into ddt
                                //from resultDDT in ddt.DefaultIfEmpty()
                                where d.Id == request.Id
                                select new SearchedDocumentModel
                                {
                                    Id = d.Id,
                                    Description = d.Description,
                                    //DocumentTypeId = d.DocumentTypeId,
                                    //DocumentTypeText = resultDDT.Name,
                                    Path = d.Path,
                                    DownloadDateText = PersianLibrary.PersianDate.GetFormatedString(d.LastDownloadDate),
                                    UploadDateText = PersianLibrary.PersianDate.GetFormatedString(d.UploadDate)

                                }).OrderByDescending(c => c.CreatedOn).ToListAsync(cancellationToken);
            }
            else if (request.RecordId != null)
            {
                result = await (from d in _context.Document
                                //join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id into ddt
                                //from resultDDT in ddt.DefaultIfEmpty()
                                where d.RecordId == request.RecordId
                                select new SearchedDocumentModel
                                {
                                    Id = d.Id,
                                    Description = d.Description,
                                    //DocumentTypeId = d.DocumentTypeId,
                                    //DocumentTypeText = resultDDT.Name,
                                    Path = d.Path,
                                    DownloadDateText = PersianLibrary.PersianDate.GetFormatedString(d.LastDownloadDate),
                                    UploadDateText = PersianLibrary.PersianDate.GetFormatedString(d.UploadDate)
                                }).OrderByDescending(c => c.CreatedOn).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
