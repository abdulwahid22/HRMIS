using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using NHCM.Domain;
using NHCM.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Persistence.Extensions;
using NHCM.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using NHCM.Application.Recruitment.Queries;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Common;
using NHCM.Application.Document.Models;
using NHCM.Application.Document.Queries;

namespace NHCM.Application.Document.Commands
{
    public class CreateDocumentCommand : IRequest<List<SearchedDocumentModel>>
    {

        // public Person Person { get; set; }

        public int? Id { get; set; }

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

    }



    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, List<SearchedDocumentModel>>
    {

        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public CreateDocumentCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<List<SearchedDocumentModel>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            List<SearchedDocumentModel> result = new List<SearchedDocumentModel>();
            // Save
            if (request.Id == null || request.Id == default(decimal))
            {
                using (_context)
                {
                    Documents d = new Documents()
                    {
                        ContentType = request.ContentType,
                        UploadDate = DateTime.Now,
                      //  Module = request.Module,
                      //  Item = request.Item,
                        RecordId = request.RecordId,
                        Root = request.Root,
                        Path = request.Path,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = "",
                        CreatedOn = DateTime.Now,
                        CreatedBy = 1,
                        EncryptionKey = "",
                        ReferenceNo = "",
                        StatusId = 1,
                        Description = request.Description,
                     //   DocumentTypeId = request.DocumentTypeId,
                        LastDownloadDate = DateTime.Now
                    };
                    _context.Document.Add(d);
                  
                    await _context.SaveChangesAsync(cancellationToken);
                    
                    result = await _mediator.Send(new SearchDocumentQuery() { Id = d.Id });
                    return result;
                }
            }
            // Update
            else
            {
                using (_context)
                {
                    Documents d = (from p in _context.Document
                                   where p.Id == request.Id
                                   select p).First();
                    d.ContentType = request.ContentType;
                    d.Path = request.Path;
                    d.ModifiedOn = DateTime.Now;
                    d.ModifiedBy = "1";
                    d.EncryptionKey = "";
                    d.StatusId = 1;
                    d.Description = request.Description;
            //        d.DocumentTypeId = request.DocumentTypeId;
                    d.LastDownloadDate = DateTime.Now;
                    
                    await _context.SaveChangesAsync();
                 
                    result = await _mediator.Send(new SearchDocumentQuery() { Id = d.Id });
                    return result;
                }
            }
        }
    }
}
