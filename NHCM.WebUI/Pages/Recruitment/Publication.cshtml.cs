using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Application.Recruitment.Commands;
using NHCM.Application.Recruitment.Queries;
using NHCM.WebUI.Types;
using NHCM.Application.Recruitment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Domain.Entities;
using NHCM.Application.Lookup.Queries;

namespace NHCM.WebUI.Pages.Recruitment
{
    public class PublicationModel : BasePage
    {
        public async Task  OnGetAsync()
        {
            ListOfPublicationType = new List<SelectListItem>();
            List<PublicationType> publicationTypes = new List<PublicationType>();
            publicationTypes = await Mediator.Send(new GetPublicationTypeQuery() { ID = null });
            foreach (PublicationType publicationType in publicationTypes)
                ListOfPublicationType.Add(new SelectListItem() { Text = publicationType.Dari, Value = publicationType.Id.ToString() });
        }

        public async Task<IActionResult> OnPostSave([FromBody] SavePersonPublicationCommand command)
        {
            try
            {

                command.CreatedBy = 10;
                command.ModifiedBy = "Test";
                command.CreatedOn = DateTime.Now;
                command.ModifiedOn = DateTime.Now;

                List<SearchedPersonPublication> result = new List<SearchedPersonPublication>();
                result = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "انتشار کارمند موفقانه ثبت سیستم شد",
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonPublicationQuery query)
        {
            try
            {
                List<SearchedPersonPublication> dbResult = new List<SearchedPersonPublication>();
                dbResult = await Mediator.Send(query);

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