using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using NHCM.Persistence.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Employment.Queries
{
   public class GetEmployeePhotoPath : IRequest<string>
    {
        public string HrCode { get; set; }
    }
    public class GetEmployeePhotoPathHandler : IRequestHandler<GetEmployeePhotoPath, string>
    {
        private readonly HCMContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMediator _mediator;
        public GetEmployeePhotoPathHandler(HCMContext context, ICurrentUser currentUser, IMediator mediator)
        {
            _context = context;
            _currentUser = currentUser;
            _mediator = mediator;
        }
        public async Task<string> Handle(GetEmployeePhotoPath request, CancellationToken cancellationToken)
        {
            string PhotoPath = null;

            if (!string.IsNullOrEmpty(request.HrCode))
                PhotoPath = await (from p in _context.Person where p.Hrcode == request.HrCode select p.PhotoPath).SingleOrDefaultAsync();
            else
                throw new BusinessRulesException("کودکادری خالی بوده نمیتواند");

            return PhotoPath ?? throw new BusinessRulesException("عکس کارمند تنظیم نگردیده است");
        }
    }
}
