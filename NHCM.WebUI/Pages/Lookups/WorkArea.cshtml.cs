using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Application.Lookup.Commands;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Lookups
{
    public class WorkAreaModel : BasePage
    {
        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostSave([FromBody]SaveWorkAreaCommand command)
        {
            try
            {
                List<SearchedWorkAreaModel> dbResult = new List<SearchedWorkAreaModel>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "معلومات موفقانه ثبت سیستم شد",
                    Description = string.Empty

                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.StateExceptionTitle(ex),
                    Description = CustomMessages.DescribeException(ex)
                });
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchWorkAreaQuery query)
        {
            try
            {
                List<SearchedWorkAreaModel> result = new List<SearchedWorkAreaModel>();

                result = await Mediator.Send(query);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
    }
}