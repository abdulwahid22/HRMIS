using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NHCM.Domain.Entities;
using NHCM.Application.Recruitment.Models;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Queries
{
   public  class SearchPersonPublicationQuery : IRequest<List<SearchedPersonPublication>>
    {
        public decimal? Id { get; set; }
        public decimal? PersonId { get; set; }
        public short PublicationTypeId { get; set; }
        public string Subject { get; set; }
        public DateTime PublishDate { get; set; }

        public String PublishDateText { get; set; }
        public string ReferenceNo { get; set; } 
        public string Isbn { get; set; }
        public int? NoofPages { get; set; }

    }

    public class SearchPersonPublicationQueryHandler : IRequestHandler<SearchPersonPublicationQuery, List<SearchedPersonPublication>>
    {


        private HCMContext _context;
        public SearchPersonPublicationQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedPersonPublication>> Handle(SearchPersonPublicationQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonPublication> result = new List<SearchedPersonPublication>();


            if (request.Id != null)
            {


                result = await (from pu in _context.Publication
                                join pt in _context.PublicationType on pu.PublicationTypeId equals pt.Id

                                where pu.Id == request.Id
                                select new SearchedPersonPublication
                                {


                                    Id = pu.Id,
                                    PersonId = pu.PersonId,
                                    PublicationTypeId = pu.PublicationTypeId,
                                    Subject = pu.Subject,
                                    PublishDate = pu.PublishDate,
                                    PublishDateText = PersianLibrary.PersianDate.GetFormatedString(pu.PublishDate),
                                    ReferenceNo = pu.ReferenceNo,
                                    Isbn = pu.Isbn,
                                    NoofPages = pu.NoofPages,

                                    PublicationTypeText = pt.Dari


                                }).OrderBy(p => p.PublishDate).ToListAsync(cancellationToken);
            }


            else if (request.PersonId != null)
            {
                result = await (from pu in _context.Publication
                                join pt in _context.PublicationType on pu.PublicationTypeId equals pt.Id

                                where pu.PersonId == request.PersonId
                                select new SearchedPersonPublication
                                {


                                    Id = pu.Id,
                                    PersonId = pu.PersonId,
                                    PublicationTypeId = pu.PublicationTypeId,
                                    Subject = pu.Subject,
                                    PublishDate = pu.PublishDate,

                                    PublishDateText = PersianLibrary.PersianDate.GetFormatedString(pu.PublishDate),
                                    ReferenceNo = pu.ReferenceNo,
                                    Isbn = pu.Isbn,
                                    NoofPages = pu.NoofPages,

                                    PublicationTypeText = pt.Dari


                                }).OrderBy(p => p.PublishDate).ToListAsync(cancellationToken);
            }






            return result;
        }
    }
}
