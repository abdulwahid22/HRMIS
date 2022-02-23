using MediatR;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Infrastructure.Exceptions;

namespace NHCM.Application.Lookup.Commands
{
    public class SaveOrganiztionCommand : IRequest<List<SearchedOrganizationModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public short OrgUnitTypeId { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public int Reference { get; set; }
    }

    public class SaveOrganiztionCommandHandler : IRequestHandler<SaveOrganiztionCommand, List<SearchedOrganizationModel>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public SaveOrganiztionCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<List<SearchedOrganizationModel>> Handle(SaveOrganiztionCommand request, CancellationToken cancellationToken)
        {
            List<SearchedOrganizationModel> result = new List<SearchedOrganizationModel>();
            if (request.Id == default(decimal))
            { 
                List<Organization> List = _context.Organization.Where(o => o.Dari == request.Dari).ToList();
                if (List.Any())
                {
                    throw new BusinessRulesException("ارگان انتخاب شده از قبل در سیستم وجود دارد");
                }
                Organization organizations = new Organization()
                {
                    Name = request.Name,
                    Dari = request.Dari,
                    Pashto = request.Pashto,
                    OrgUnitTypeId = request.OrgUnitTypeId,
                    Code = request.Code,
                    StatusId = request.StatusId
                };
                _context.Organization.Add(organizations);
                await _context.SaveChangesAsync(cancellationToken);
                result = await _mediator.Send(new SearchOrganizationQuery() { Id = organizations.Id });
            }
            else
            {
                Organization UpdateRecord = await _context.Organization.Where(or => or.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                UpdateRecord.Name = request.Name;
                UpdateRecord.Dari = request.Dari;
                UpdateRecord.Pashto = request.Pashto;
                UpdateRecord.OrgUnitTypeId = request.OrgUnitTypeId;
                UpdateRecord.Code = request.Code;
                UpdateRecord.StatusId = request.StatusId;
                await _context.SaveChangesAsync(cancellationToken);
                result = await _mediator.Send(new SearchOrganizationQuery() { Id = UpdateRecord.Id });
            }
            return result; 
        }
    } 
}
