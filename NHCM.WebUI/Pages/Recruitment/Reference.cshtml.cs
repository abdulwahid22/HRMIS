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
    public class ReferenceModel : BasePage
    {
        public async Task  OnGetAsync()
        {
            ListOfReferenceType = new List<SelectListItem>();
            List<ReferenceType> referenceTypes = new List<ReferenceType>();
            referenceTypes = await Mediator.Send(new GetReferenceTypeQuery() { ID = null });
            foreach (ReferenceType refe in referenceTypes)
                ListOfReferenceType.Add(new SelectListItem() { Text = refe.Name, Value = refe.Id.ToString() });


            //Get Locations
            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));
        }
        public async Task<IActionResult> OnPostSave([FromBody]SavePersonReferenceCommand command)
        {
            try
            {
                command.CreatedBy = 10;
                command.ModifiedBy = "Test";
                command.CreatedOn = DateTime.Now;
                command.ModifiedOn = DateTime.Now;

                List<SearchedPersonReference> dbResult = new List<SearchedPersonReference>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {
                    Data = new { list = dbResult },
                    Status = NHCM.WebUI.Types.UIStatus.Success,
                    Text = "  تضمین کارمند موفقانه ثبت سیستم شد",
                    Description = string.Empty

                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.StateExceptionTitle(ex),
                    Description = CustomMessages.DescribeException(ex)

                });
            }

        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonReferenceQuery query)
        {

            try
            {
                List<SearchedPersonReference> result = new List<SearchedPersonReference>();
                result = await Mediator.Send(query);

                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {
                    Data = new { list = result },
                    Status = NHCM.WebUI.Types.UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty

                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {

                    Data = null,
                    Status = NHCM.WebUI.Types.UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,

                    Description = ex.Message
                });
            }
        }

    }
}