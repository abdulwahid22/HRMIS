using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Recruitment.Commands;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Recruitment
{
    public class EducationModel : BasePage
    {
        public async Task OnGetAsync()
        {
            //Get Locations
            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));

            ListOfEducationLevels = new List<SelectListItem>();
            List<EducationLevel> educationLevels = new List<EducationLevel>();
            educationLevels = await Mediator.Send(new GetEducationLevelQuery() { ID = null });
            foreach (EducationLevel level in educationLevels)
                ListOfEducationLevels.Add(new SelectListItem() { Text = level.Name, Value = level.Id.ToString() });
        }


       
        public async Task<IActionResult> OnPostSave([FromBody] SavePersonEducation command)
        {
            try
            {
                command.CreatedBy = 10; // UNTILL : application of Identity
                command.CreatedOn = DateTime.Now;
                command.ModifiedBy = "Test";
                command.ModifiedOn = DateTime.Now;
                List<SearchedPersonEducations> dbResult = new List<SearchedPersonEducations>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "اطلاعات تحصیلی فرد موفقانه ثبت سیستم شد",
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




        public async Task<IActionResult> OnPostDelete([FromBody] DeletePersonEducationCommand command)
        {
            try
            {
                string dbResult = string.Empty;
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "اطلاعات تحصیلی انتخاب شده موفقانه حذف  شد",
                    Description = string.Empty

                });


            }
            catch (Exception ex)
            {
                return new JsonResult(new UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace

                });
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonEducationQuery searchQuery)
        {
            try
            {
                List<SearchedPersonEducations> dbResult = new List<SearchedPersonEducations>();
                dbResult = await Mediator.Send(searchQuery);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty

                });


            }
            catch (Exception ex)
            {
                return new JsonResult(new UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace

                });
            }
        }


    }
}