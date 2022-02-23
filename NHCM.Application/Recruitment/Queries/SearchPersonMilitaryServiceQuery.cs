using MediatR;
using NHCM.Application.Recruitment.Models;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Recruitment.Queries
{
    public class SearchPersonMilitaryServiceQuery : IRequest<List<SearchedPersonMilitaryService>>
    {
        public int? Id { get; set; }
        public decimal? PersonId { get; set; }

        public int MilitaryServiceTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }

        public String StartDateText { get; set; }
        public String EndDateText { get; set; }
    }


    public class SearchPersonMilitaryServiceQueryHandler : IRequestHandler<SearchPersonMilitaryServiceQuery, List<SearchedPersonMilitaryService>>
    {
        private HCMContext _context;
        public SearchPersonMilitaryServiceQueryHandler(HCMContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedPersonMilitaryService>> Handle(SearchPersonMilitaryServiceQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonMilitaryService> result = new List<SearchedPersonMilitaryService>();

            if (request.Id != null)
            {
                result = await (from ms in _context.MilitaryService
                                where ms.Id == request.Id
                                select new SearchedPersonMilitaryService
                                {
                                    Id = ms.Id,
                                    PersonId = ms.PersonId,
                                    MilitaryServiceTypeId = ms.MilitaryServiceTypeId,
                                    StartDate = ms.StartDate,
                                    EndDate = ms.EndDate,
                                    Remark = ms.Remark,
                                    StartDateText = PersianLibrary.PersianDate.GetFormatedString(ms.StartDate),
                                    EndDateText = PersianLibrary.PersianDate.GetFormatedString(ms.EndDate),
                                    // CHANGE : apply join when look.MilitaryServiceType table is added and scaffolded in system
                                    MilitaryServiceTypeText = request.MilitaryServiceTypeId == 1 ? "مکلفیت" : "احتیاط"
                                }).ToListAsync(cancellationToken);
            }

            else if (request.PersonId != null)
            {
                result = await (from ms in _context.MilitaryService
                                where ms.PersonId == request.PersonId
                                select new SearchedPersonMilitaryService
                                {
                                    Id = ms.Id,
                                    PersonId = ms.PersonId,
                                    MilitaryServiceTypeId = ms.MilitaryServiceTypeId,
                                    StartDate = ms.StartDate,
                                    EndDate = ms.EndDate,
                                    Remark = ms.Remark,
                                    StartDateText = PersianLibrary.PersianDate.GetFormatedString(ms.StartDate),
                                    EndDateText = PersianLibrary.PersianDate.GetFormatedString(ms.EndDate),
                                    // CHANGE : apply join when look.MilitaryServiceType table is added and scaffolded in system
                                    MilitaryServiceTypeText = request.MilitaryServiceTypeId == 1 ? "مکلفیت" : "احتیاط"
                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
