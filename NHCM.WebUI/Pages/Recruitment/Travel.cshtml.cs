using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using NHCM.Application.Lookup.Queries;
using NHCM.WebUI.Types;
using NHCM.Domain.Entities;
using NHCM.Application.Recruitment.Queries;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Commands;

namespace NHCM.WebUI.Pages.Recruitment
{
    public class TravelModel : BasePage
    {

       // public List<SelectListItem> ListOfLocations = new List<SelectListItem>();
        public  async Task  OnGetAsync()
        {
            //Get Locations
            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));
        }
        public async Task<IActionResult> OnPostSave([FromBody]SavePersonTravelCommand command)
        {
            try
            {
                

                List<SearchedPersonTravel> dbResult = new List<SearchedPersonTravel>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new Types.UIResult()
                {

                    Data = new { list = dbResult },
                    Status = Types.UIStatus.Success,
                    Text = "اطلاعات سفرهای فرد موفقانه ثبت سیستم شد",
                    Description = string.Empty

                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new Types.UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.StateExceptionTitle(ex),
                    Description = CustomMessages.DescribeException(ex)

                });
            }

        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonTravelQuery command)
        {
            try
            {
                List<SearchedPersonTravel> dbResult = new List<SearchedPersonTravel>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new Types.UIResult()
                {

                    Data = new { list = dbResult },
                    Status = Types.UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty

                });


            }
            catch (Exception ex)
            {
                return new JsonResult(new Types.UIResult()
                {

                    Data = null,
                    Status = Types.UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace

                });
            }
        }



        public async Task<IActionResult> OnPostDelete([FromBody]DeletePersonTravelCommand command)
        {
            try
            {
                
                string dbResult  = await Mediator.Send(command);

                return new JsonResult(new Types.UIResult()
                {

                    Data = new { list = dbResult },
                    Status = Types.UIStatus.Success,
                    Text = "اطلاعات مرتبط به سفر کارمند موفقانه حذف گردید",
                    Description = string.Empty

                });


            }
            catch (Exception ex)
            {
                return new JsonResult(new Types.UIResult()
                {

                    Data = null,
                    Status = Types.UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace

                });
            }
        }


    }
}