using MediatR;
using NHCM.Application.Organogram.Models;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NHCM.Application.Organogram.Commands
{
    public class ConfirmPlanCommand : IRequest<List<SearchedPlan>>
    {
        public int? Id { get; set; }
        public int OrganizationId { get; set; }
        public short StatusId { get; set; }
    }

    public class ConfirmPlanCommanHandler : IRequestHandler<ConfirmPlanCommand, List<SearchedPlan>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public ConfirmPlanCommanHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }

        public async Task<List<SearchedPlan>> Handle(ConfirmPlanCommand request, CancellationToken cancellationToken)
        {

            List<SearchedPlan> result = new List<SearchedPlan>();


            if ( request.Id != default(int))
            {
                using (_context)
                { 
                    OrganoGram toUpdateRecord = await (from org in _context.OrganoGram
                                                       where org.Id == request.Id
                                                       select org).SingleOrDefaultAsync();

                    toUpdateRecord.StatusId = request.StatusId;

                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new Queries.SearchPlanQuery() { Id = toUpdateRecord.Id});
                }
            } 

           return result;
        }
    }
}
