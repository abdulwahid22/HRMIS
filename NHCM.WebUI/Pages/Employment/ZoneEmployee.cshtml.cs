using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Employment.Commands;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.ProcessTracks.Commands;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence.Infrastructure.Identity;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Employment
{
    public class ZoneEmployeeModel : BasePage
    {
        private readonly UserManager<HCMUser> _userManager;
        public int? SignedInUserOrganizationID { get; set; }

        public ZoneEmployeeModel(UserManager<HCMUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            SignedInUserOrganizationID = ((HCMUser)await _userManager.GetUserAsync(User)).OrganizationID;

            // Get List Of  Persons
            ListOfPerson = new List<SelectListItem>();
            List<SearchedPersonModel> searchedPeople = new List<SearchedPersonModel>();

            searchedPeople = await Mediator.Send(new SearchPersonQuery() { OrganizationId = SignedInUserOrganizationID });

            foreach (SearchedPersonModel person in searchedPeople)
            {
                ListOfPerson.Add(new SelectListItem()
                {
                    Text = new StringBuilder() { }.Append(person.FirstName).Append(" فرزند ").Append(" ").Append(person.FatherName).ToString(),
                    Value = person.Id.ToString()
                });
            }

            ListOfZones = new List<SelectListItem>();
            List<Zones> nsiazone = new List<Zones>();
            nsiazone = await Mediator.Send(new GetZonesQuery() { ID = null});
            foreach (Zones zo in nsiazone)
                ListOfZones.Add(new SelectListItem() { Text = zo.Name, Value = zo.ID.ToString() });
        }


        public async Task<IActionResult> OnPostSave([FromBody]SaveZoneEmployeesCommand command)
        {
            try
            {
                List<SearchedZoneModel> dbResult = new List<SearchedZoneModel>();
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchZoneEmployeeQuery query)
        {
            try
            {
                List<SearchedZoneModel> result = new List<SearchedZoneModel>();

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