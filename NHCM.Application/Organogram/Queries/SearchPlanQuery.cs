using MediatR;
using NHCM.Application.Organogram.Models;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Organogram.Queries
{
    public class SearchPlanQuery : IRequest<List<SearchedPlan>>
    {
        public int? Id { get; set; }
        public int? OrganizationId { get; set; }
        public int? StatusId { get; set; }
        public int? Year { get; set; }
        public int? NumberOfPositions { get; set; }
        public int? IsPositionsCopied { get; set; }
        public string CreationType { get; set; }
        public string StatusText { get; set; }
        public string OrganizationText { get; set; }
         
        public int ProccessID { get; set; }
    }


    public class SearchOrganogramQueryHandler : IRequestHandler<SearchPlanQuery, List<SearchedPlan>>
    {
        private HCMContext _context;
        public SearchOrganogramQueryHandler(HCMContext context) { _context = context; }

        public async Task<List<SearchedPlan>> Handle(SearchPlanQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPlan> result = new List<SearchedPlan>();
            if (request.OrganizationId != null && request.Year != null)
            {
                result = await (from Organogram in _context.OrganoGram
                                join Orgunit in _context.Organization on Organogram.OrganizationId equals Orgunit.Id into orgs
                                from resultOrgUnit in orgs.DefaultIfEmpty()

                                join prTr in _context.Process on request.StatusId equals prTr.Id into proccessT
                                from resultprTr in proccessT.DefaultIfEmpty()

                                where Organogram.OrganizationId == request.OrganizationId && Organogram.Year == request.Year 
                                select new SearchedPlan
                                {
                                    Id = Organogram.Id,
                                    OrganizationId = Organogram.OrganizationId,
                                    StatusId = Organogram.StatusId,   ////
                                    Year = Organogram.Year,
                                    IsPositionsCopied= Organogram.IsPositionsCopied,
                                    NumberOfPositions = Organogram.NumberOfPositions,
                                    StatusText = resultprTr.Name,
                                    OrganizationText = resultOrgUnit.Dari,
                                    CreationType = Organogram.IsPositionsCopied.Equals(1) ? "انتقال از سال " + (Organogram.Year - 1).ToString() : "ترتیب جدید"
                                }).ToListAsync(cancellationToken);

            }
            else if (request.Id != null || request.OrganizationId != null)
            {
                result = await (from Organogram in _context.OrganoGram
                                join organization in _context.Organization on Organogram.OrganizationId equals organization.Id into orgs
                                from resultOrgUnit in orgs.DefaultIfEmpty()

                                join prTr in _context.Process on request.StatusId equals prTr.Id into proccessT
                                from resultprTr in proccessT.DefaultIfEmpty()
                                 
                                where Organogram.Id == request.Id || Organogram.OrganizationId == request.OrganizationId
                                select new SearchedPlan
                                {
                                    Id = Organogram.Id,
                                    OrganizationId = Organogram.OrganizationId,
                                    StatusId = Organogram.StatusId,
                                    Year = Organogram.Year,
                                    IsPositionsCopied = Organogram.IsPositionsCopied,
                                    NumberOfPositions = Organogram.NumberOfPositions,
                                    StatusText = resultprTr.Name,
                                    OrganizationText = resultOrgUnit.Dari,
                                    CreationType = Organogram.IsPositionsCopied.Equals(1) ? "انتقال از سال " + (Organogram.Year - 1).ToString() : "ترتیب جدید"
                                }).ToListAsync(cancellationToken);
            }
            else
            {
                result = await (from Organogram in _context.OrganoGram
                                join Orgunit in _context.Organization on Organogram.OrganizationId equals Orgunit.Id into orgs
                                from resultOrgUnit in orgs.DefaultIfEmpty()

                                join prTr in _context.Process on request.StatusId equals prTr.Id into proccessT
                                from resultprTr in proccessT.DefaultIfEmpty()
                                 

                                //where  resultprTr.Id == request.ProccessID
                                select new SearchedPlan
                                {
                                    Id = Organogram.Id,
                                    OrganizationId = Organogram.OrganizationId,
                                    StatusId = Organogram.StatusId,
                                    Year = Organogram.Year,
                                    IsPositionsCopied = Organogram.IsPositionsCopied,
                                    NumberOfPositions = Organogram.NumberOfPositions,
                                    StatusText = resultprTr.Name,
                                    OrganizationText = resultOrgUnit.Dari,
                                    CreationType = Organogram.IsPositionsCopied.Equals(1) ? "انتقال از سال " + (Organogram.Year - 1).ToString() : "ترتیب جدید"
                                }).ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
