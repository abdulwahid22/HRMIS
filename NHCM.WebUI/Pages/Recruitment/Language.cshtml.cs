using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NHCM.WebUI.Types;
using NHCM.Application.Recruitment.Commands;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Application.Lookup.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NHCM.WebUI.Pages.Recruitment
{
    public class LanguageModel : BasePage
    {
       

       


       
        public  async Task  OnGetAsync()
        {
            ListOfLanguages = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            List<Language> languages = new List<Language>();
            languages = await Mediator.Send(new GetLanguageQuery() { ID = null });
            foreach (Language language in languages)
                ListOfLanguages.Add(new SelectListItem(language.Name, language.Id.ToString()));

            ListOfExpertise = new List<SelectListItem>();
            List<Expertise> expertises = new List<Expertise>();
            expertises = await Mediator.Send(new GetExpertiseQuery() { ID = null });
            foreach (Expertise expertise in expertises)
                ListOfExpertise.Add(new SelectListItem() { Text = expertise.Name, Value = expertise.Id.ToString() });

        }


        public async Task<IActionResult> OnPostSave([FromBody] SavePersonLanguageCommand command)
        {
            try
            {
                List<SearchedPersonLanguage> dbResult = new List<SearchedPersonLanguage>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult() {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "اطلاعات زبان فرد موفقانه ثبت سیستم شد",
                    Description = string.Empty

                });


            }
            catch(Exception ex)
            {
                return new JsonResult(new UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException ,
                    Description = ex.Message + " \n StackTrace : "+ ex.StackTrace

                });
            }
        }




        public async Task<IActionResult> OnPostDelete([FromBody] DeletePersonLanguageCommand command)
        {
            try
            {
                string dbResult= string.Empty;
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "زبان انتخاب شده موفقانه حذف گردید",
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonLanguageQuery command)
        {
            try
            {
                List<SearchedPersonLanguage> dbResult = new List<SearchedPersonLanguage>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text =string.Empty,
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