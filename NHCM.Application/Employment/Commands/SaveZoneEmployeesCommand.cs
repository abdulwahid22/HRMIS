using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Employment.Commands
{
    public class SaveZoneEmployeesCommand : IRequest<List<SearchedZoneModel>>
    {
        public int? ID { get; set; }
        public decimal? PersonID { get; set; }
        public decimal? ZoneID { get; set; }
        public int Code { get; set; }
    }
    public class SaveZoneEmployeesCommandHandler : IRequestHandler<SaveZoneEmployeesCommand, List<SearchedZoneModel>>
    {
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public SaveZoneEmployeesCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }
        public async Task<List<SearchedZoneModel>> Handle(SaveZoneEmployeesCommand request, CancellationToken cancellationToken)
        {
            List<SearchedZoneModel> result = new List<SearchedZoneModel>();
            HCMContext _db = new HCMContext();
            var count = _db.ZoneEmployees.Count(x => x.PersonID.Equals(request.PersonID));
            if (count.Equals(0))
            {
                if (request.ID == null || request.ID == default(int))
                {
                    //result = await _mediator.Send(new SearchZoneEmployeeQuery() { ID = request.ID });
                    //if (result.Any())
                    //{
                    //    throw new BusinessRulesException(" شخص انتخاب شده از قبل در سیستم موجود است");
                    //}
                    var lastCount = _context.ZoneEmployees.Where(x => x.ZoneID == request.ZoneID).Count();

                    using (_context)
                    {
                        ZoneEmployees zemployee = new ZoneEmployees()
                        {

                            PersonID = request.PersonID,
                            ZoneID = request.ZoneID,
                            Code = lastCount + 1
                        };
                        _context.ZoneEmployees.Add(zemployee);
                        await _context.SaveChangesAsync(cancellationToken);

                        result = await _mediator.Send(new SearchZoneEmployeeQuery() { ID = zemployee.ID });
                    }
                }
            }
          
            else
            {

                List<ZoneEmployees> results = (from p in _db.ZoneEmployees
                                              where p.PersonID.Equals(request.PersonID)
                                              select p).ToList();

                foreach (ZoneEmployees p in results)
                {
                    p.PersonID = request.PersonID;
                    p.ZoneID = request.ZoneID;
                    p.Code = p.Code;
                    result = await _mediator.Send(new SearchZoneEmployeeQuery() { ID = p.ID });
                }

                _db.SaveChanges();
            }
            return result;
        }
    }

}
