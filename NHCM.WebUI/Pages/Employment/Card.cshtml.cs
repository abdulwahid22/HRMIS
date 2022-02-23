using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Employment.Commands;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Persistence.Infrastructure.Identity;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Employment
{
    public class CardModel : BasePage
    {

        private readonly UserManager<HCMUser> _userManager;
        public int? SignedInUserOrganizationID { get; set; }

        public CardModel(UserManager<HCMUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGet()
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
        }
        public async Task<IActionResult> OnPostSave([FromBody] SetIdentityCardCommand command)
        {
        

            List<SearchedIdentityCardModel> listOfCards = new List<SearchedIdentityCardModel>();

            try
            {
                listOfCards = await Mediator.Send(command);
                return new JsonResult(new UIResult() {
                    Data = new { list = listOfCards },
                    Status = UIStatus.Success,
                    Text = "اطلاعات کارت هویت موفقانه تنظیم گردید",
                    Description = string.Empty
                });
            }
            catch(Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] GetIdentityCardsQuery searchQuery)
        {

            try
            {

                List<SearchedIdentityCardModel> listOfCards = new List<SearchedIdentityCardModel>();
                listOfCards = await Mediator.Send(searchQuery);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = listOfCards },
                    Status = UIStatus.SuccessWithoutMessage,
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