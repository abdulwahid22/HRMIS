using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Accounts.Commands;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Persistence.Infrastructure.Identity;
using NHCM.Persistence.Infrastructure.Services;
using NHCM.WebUI.Types;
namespace NHCM.WebUI.Pages.Security
{
    [Authorize("UserRegistrar")]
    public class RegisterModel : BasePage
    {

        private readonly UserManager<HCMUser> _userManager;
        public int? SignedInUserOrganizationID { get; set; }

        public RegisterModel(UserManager<HCMUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {

            SignedInUserOrganizationID = ((HCMUser)await _userManager.GetUserAsync(User)).OrganizationID;

            //Get Organization
            ListOfOrganization = new List<SelectListItem>();
            List<Organization> organizations = new List<Organization>();
            organizations = await Mediator.Send(new GetOrganiztionQuery() { Id = SignedInUserOrganizationID ?? default(int) });
            foreach (Organization organization in organizations)
                ListOfOrganization.Add(new SelectListItem(organization.Dari, organization.Id.ToString()));


            // Get List Of  Persons
            ListOfPerson = new List<SelectListItem>();
            List<SearchedPersonModel> searchedPeople = new List<SearchedPersonModel>();
            // CHANGE: change the SearchPersonQuery Request to include a property to bring all records not only 10000
            searchedPeople = await Mediator.Send(new SearchPersonQuery() { OrganizationId = SignedInUserOrganizationID, NoOfRecords = 10000 });

            foreach (SearchedPersonModel person in searchedPeople)
            {
                ListOfPerson.Add(new SelectListItem()
                {
                    Text = new StringBuilder() { }.Append(person.FirstName).Append(" فرزند ").Append(" ").Append(person.FatherName).ToString(),
                    Value = person.Id.ToString()
                });
            }
        }
        public async Task<IActionResult> OnPostSave([FromBody] CreateUserCommand command)
        {
            try
            {
                List<string> result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "کاربر موفقانه ثبت سیستم شد" + "\n" + "رمز عبور: " +  result[0].ToString(),
                    Description = string.Empty

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

        public void OnPost()
        {
        }
    }
}