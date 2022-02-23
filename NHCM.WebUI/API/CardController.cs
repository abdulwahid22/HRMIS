using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.WebUI.Types;
using NHCM.Application.Document.Disk.FileManager;
using Microsoft.Extensions.Configuration;
using NHCM.Domain.ViewsEntities;

namespace NHCM.WebUI.API
{

    [Route("api/[controller]/[action]/{hrCode?}")]
    public class CardController : Controller
    {
        
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        

        public CardController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfo([FromRoute]string hrCode)
        {

            List<carddetails> cardInfo = new List<carddetails>();

            cardInfo = await _mediator.Send(new GetCardDataQuery() { HrCode = hrCode });
            
            return new JsonResult(new UIResult()
            {
                Data = new { list = cardInfo },
                Status = UIStatus.Success,
                Text = string.Empty,
                Description = string.Empty
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeImage([FromRoute] string hrCode)
        {
            try
            {
                string fileName = await _mediator.Send(new GetEmployeePhotoPath() { HrCode = hrCode });

                FileStorage fileStorage = new FileStorage();
                string FilePath = _configuration["Photo"] + fileName;

                System.IO.Stream filecontent = await fileStorage.GetAsync(FilePath);
                var filetype = fileStorage.GetContentType(FilePath);
                return File(filecontent, filetype, fileName);

            }
            catch(Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
    }
}
