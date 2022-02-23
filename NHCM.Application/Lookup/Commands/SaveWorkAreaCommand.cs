using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Commands
{
    public class SaveWorkAreaCommand : IRequest<List<SearchedWorkAreaModel>>
    {
        public decimal? Id { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
    }

    public class SaveWorkAreaCommandHandler : IRequestHandler<SaveWorkAreaCommand, List<SearchedWorkAreaModel>>
    { 
        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public SaveWorkAreaCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }
        public async Task<List<SearchedWorkAreaModel>> Handle(SaveWorkAreaCommand request, CancellationToken cancellationToken)
        {
            List<SearchedWorkAreaModel> result = new List<SearchedWorkAreaModel>();

            if (request.Id == null || request.Id == default(int))
            {
                result = await _mediator.Send(new SearchWorkAreaQuery() { Title = request.Title});
                if (result.Any())
                {
                    throw new BusinessRulesException("ساحه کاری انتخاب شده از قبل در سیستم موجود است");
                }

                using (_context)
                {
                    WorkArea workarea = new WorkArea()
                    {
                      Title = request.Title,
                      TitleEng = request.TitleEng
                    };
                    _context.WorkArea.Add(workarea);
                    await _context.SaveChangesAsync(cancellationToken);

                    result = await _mediator.Send(new SearchWorkAreaQuery() { Id = workarea.Id });
                }
            }
            else
            {
                using (_context)
                {
                    WorkArea toUpdateRecord = await _context.WorkArea.Where(or => or.Id == request.Id).SingleOrDefaultAsync();
                    toUpdateRecord.Title = request.Title;
                    toUpdateRecord.TitleEng = request.TitleEng; 
                    await _context.SaveChangesAsync(cancellationToken);
                    result = await _mediator.Send(new SearchWorkAreaQuery() { Id = toUpdateRecord.Id });
                }
            }
            return result;
        }
    }
}
