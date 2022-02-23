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
    public class PersonSkillModel : BasePage
    {
        public async Task  OnGetAsync()
        {
            //Get Locations
            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));


            ListOfSkillType = new List<SelectListItem>();
            List<SkillType> skillTypes = new List<SkillType>();
            skillTypes = await Mediator.Send(new GetSkillTypeQuery() { ID = null });
            foreach (SkillType skillType in skillTypes)
                ListOfSkillType.Add(new SelectListItem() { Text = skillType.Name, Value = skillType.Id.ToString() });

            ListOfExpertise = new List<SelectListItem>();
            List<Expertise> expertises = new List<Expertise>();
            expertises = await Mediator.Send(new GetExpertiseQuery() { ID = null });
            foreach (Expertise expertise in expertises)
                ListOfExpertise.Add(new SelectListItem() { Text = expertise.Name, Value = expertise.Id.ToString() });


            ListOfCertification = new List<SelectListItem>();
            List<Certification> certifications = new List<Certification>();
            certifications = await Mediator.Send(new GetCertificationQuery() { ID = null });
            foreach (Certification certification in certifications)
                ListOfCertification.Add(new SelectListItem() { Text = certification.Name, Value = certification.Id.ToString() });





        }
        public async Task<IActionResult> OnPostSave([FromBody] SavePersonSkillsCommand command)
        {
            try
            {
                command.CreatedBy = 10;
                command.ModifiedBy = "Test";
                command.CreatedOn = DateTime.Now;
                command.ModifiedOn = DateTime.Now;
               
                List<SearchedPersonSkill> dbResult = new List<SearchedPersonSkill>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "مهارت فرد موفقانه ثبت سیستم شد",
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



        public async Task<IActionResult> OnPostSearch([FromBody]SearchPersonSkillQuery command)
        {
            try
            {
                

                List<SearchedPersonSkill> dbResult = new List<SearchedPersonSkill>();
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