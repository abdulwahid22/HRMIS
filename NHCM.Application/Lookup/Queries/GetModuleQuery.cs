using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Lookup.Queries
{
    public class GetModuleQuery : IRequest<List<Module>>
    {
        
    }
    public class GetModulepQueryHandler : IRequestHandler<GetModuleQuery, List<Module>>
    {
        private readonly HCMContext _dbContext;
        public GetModulepQueryHandler(HCMContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Module>> Handle(GetModuleQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Module.ToListAsync();
        }
    }
}
