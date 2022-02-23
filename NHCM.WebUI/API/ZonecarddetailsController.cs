using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NHCM.Application.Employment.Queries;
using NHCM.Domain.ViewsEntities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.API
{
    [Route("api/[controller]/[action]/{hrCode?}")]
    [ApiController]
    public class ZonecarddetailsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public ZonecarddetailsController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfo([FromRoute] string hrCode)
        {

            List<ZoneCardDetails> cardInfo = new List<ZoneCardDetails>();

            cardInfo = await _mediator.Send(new GetZoneCardQuery() { HrCode = hrCode });

            return new JsonResult(new UIResult()
            {
                Data = new { list = cardInfo },
                Status = UIStatus.Success,
                Text = string.Empty,
                Description = string.Empty
            });
        }
    }
}
