using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Organogram.Commands
{
  public    class DeletePositionCommand : IRequest<List<SearchedPosition>>
    {

        public decimal Id { get; set; } 
    
    }

    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, List<SearchedPosition>>
    {



        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        public DeletePositionCommandHandler(HCMContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<List<SearchedPosition>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {

            List<SearchedPosition> parentOfThePositionToBeDeleted = new List<SearchedPosition>();


            List<Position> childs = await _context.Position.Where(p => p.ParentId == request.Id).ToListAsync(cancellationToken);

            if (childs.Any())
                throw new BusinessRulesException("بست انتخاب شده دارای بست های مادون میباشد، لطفاً بست های مادون را حذف نمائید، بعداً شما میتوانید که این بست را حذف نمائید");

            if(request.Id != default(decimal))
            {
                Position toDeletePosition = await _context.Position.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if(toDeletePosition != null)
                {
                    _context.Position.Remove(toDeletePosition);
                    await _context.SaveChangesAsync(cancellationToken);

                    parentOfThePositionToBeDeleted = await _mediator.Send(new SearchPositionQuery() { Id = toDeletePosition.ParentId });
                }
                else
                {
                    throw new BusinessRulesException("بست انتخاب شده در سیستم موجود نمیباشد");
                }
            }
            return parentOfThePositionToBeDeleted;
        }
    }
}
