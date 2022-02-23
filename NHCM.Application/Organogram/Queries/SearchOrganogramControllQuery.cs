using MediatR;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Organogram.Models;

namespace NHCM.Application.Organogram.Queries
{
    public class SearchOrganogramControllQuery : IRequest<List<SearchedOrganogramControll>>
    {
        public int? Id { get; set; }
        public int? OrganizationId { get; set; }
        public int StatusId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int? Year { get; set; }
        public DateTime? PreparedDate { get; set; }
        public DateTime? AppreovedDate { get; set; }
        public int? NumberOfPositions { get; set; }

        public string statustext { get; set; }
        public string OrganizationText { get; set; }
        public string TitleText { get; set; }
    }


    public class SearchOrganogramControllHandler : IRequestHandler<SearchOrganogramControllQuery, List<SearchedOrganogramControll>>
    {
        private HCMContext _context;
        public SearchOrganogramControllHandler(HCMContext context)
        {
            _context = context;
        }

        public async Task<List<SearchedOrganogramControll>> Handle(SearchOrganogramControllQuery request, CancellationToken cancellationToken)
        {

            List<SearchedOrganogramControll> result = new List<SearchedOrganogramControll>();

            if (request.OrganizationId != null)
            {
                using (_context)
                {
                    result = await(from Organogram in _context.OrganoGram

                                   join Orgunit in _context.Organization on Organogram.OrganizationId equals Orgunit.Id into orgs
                                   from resultOrgUnit in orgs.DefaultIfEmpty()

                                   join st in _context.Status on Organogram.StatusId equals st.Id into se
                                   from resultStatus in se.DefaultIfEmpty()


                                   where Organogram.OrganizationId == request.OrganizationId && Organogram.StatusId == 52
                                   select new SearchedOrganogramControll
                                   {
                                       Id = Organogram.Id,
                                       OrganizationId = Organogram.OrganizationId,
                                       StatusId = Organogram.StatusId,
                                       Year = Organogram.Year,
                                       NumberOfPositions = Organogram.NumberOfPositions,
                                       //(N'تشکیل ', S.Dari, N' سال ', O.Year, N' ', OU.Dari) Title
                                       statustext = resultStatus.Dari,
                                       OrganizationText = resultOrgUnit.Dari,
                                       TitleText = " تشکیل " + resultStatus.Dari + " سال " + Organogram.Year + " " + resultOrgUnit.Dari

                                   }).ToListAsync(cancellationToken);
                }
            }
            else if (request.Id != null)
            {
                using (_context)
                {
                    ///Change OrganoGram to Organogram
                    result = await(from Organogram in _context.OrganoGram

                                   join organization in _context.Organization on Organogram.OrganizationId equals organization.Id into orgs
                                   from resultOrgUnit in orgs.DefaultIfEmpty()

                                   join st in _context.Status on Organogram.StatusId equals st.Id into se
                                   from resultStatus in se.DefaultIfEmpty()


                                   where Organogram.Id == request.Id && Organogram.StatusId == 52
                                   select new SearchedOrganogramControll
                                   {
                                       Id = Organogram.Id,
                                       OrganizationId = Organogram.OrganizationId,
                                       StatusId = Organogram.StatusId,
                                       Year = Organogram.Year,
                                       NumberOfPositions = Organogram.NumberOfPositions,
                                       //(N'تشکیل ', S.Dari, N' سال ', O.Year, N' ', OU.Dari) Title
                                       statustext = resultStatus.Dari,
                                       OrganizationText = resultOrgUnit.Dari,
                                       TitleText = " تشکیل " + resultStatus.Dari + " سال " + Organogram.Year + " " + resultOrgUnit.Dari


                                   }).ToListAsync(cancellationToken);
                }
            }


            else
            {
                using (_context)
                {
                    result = await(from Organogram in _context.OrganoGram

                                   join Orgunit in _context.Organization on Organogram.OrganizationId equals Orgunit.Id into orgs
                                   from resultOrgUnit in orgs.DefaultIfEmpty()

                                   join st in _context.Status on Organogram.StatusId equals st.Id into se
                                   from resultStatus in se.DefaultIfEmpty()
                                   where Organogram.StatusId == 52
                                   select new SearchedOrganogramControll
                                   {
                                       Id = Organogram.Id,
                                       OrganizationId = Organogram.OrganizationId,
                                       StatusId = Organogram.StatusId,
                                       Year = Organogram.Year,
                                       NumberOfPositions = Organogram.NumberOfPositions,
                                       //(N'تشکیل ', S.Dari, N' سال ', O.Year, N' ', OU.Dari) Title
                                       statustext = resultStatus.Dari,
                                       OrganizationText = resultOrgUnit.Dari,
                                       TitleText = " تشکیل " + resultStatus.Dari + " سال " + Organogram.Year + " " + resultOrgUnit.Dari

                                   }).ToListAsync(cancellationToken);
                }
            }
            return result;
        }
    }
}
