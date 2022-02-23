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
    public class ExperienceModel : BasePage
    {
        public async Task  OnGetAsync()
        {
            ListOfOrganizationType = new List<SelectListItem>();
            List<OrganizationType> organizationTypes = new List<OrganizationType>();
            organizationTypes = await Mediator.Send(new GetOrganizationTypeQuery() { ID = null });
            foreach (OrganizationType organizationType in organizationTypes)
                ListOfOrganizationType.Add(new SelectListItem() { Text = organizationType.Name, Value = organizationType.Id.ToString() });

            //Get Locations
            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));


            ListOfRanks = new List<SelectListItem>();
            List<Rank> ranks = new List<Rank>();
            ranks = await Mediator.Send(new GetRankQuery() { ID = null });
            foreach (Rank rank in ranks)
                ListOfRanks.Add(new SelectListItem() { Text = rank.Name, Value = rank.Id.ToString() });

            ListOfJobStatus = new List<SelectListItem>();
            List<JobStatus> jobStatuses = new List<JobStatus>();
            jobStatuses = await Mediator.Send(new GetJobStatusQuery() { ID = null });
            foreach (JobStatus jobStatus in jobStatuses)
                ListOfJobStatus.Add(new SelectListItem() { Text = jobStatus.Name, Value = jobStatus.Id.ToString() });

            ListOfExperienceType = new List<SelectListItem>();
            List<ExperienceType> experienceTypes = new List<ExperienceType>();
            experienceTypes = await Mediator.Send(new GetExperienceTypeQuery() { ID = null });
            foreach (ExperienceType experienceType in experienceTypes)
                ListOfExperienceType.Add(new SelectListItem() { Text = experienceType.Dari, Value = experienceType.Id.ToString() });
        }


        public async Task<IActionResult> OnPostSave([FromBody] SavePersonExperienceCommand command)
        {
            try
            {

                command.CreatedBy = 10; // UNTILL : application of Identity
                command.CreatedOn = DateTime.Now;
                command.ModifiedBy = "Test";
                command.ModifiedOn = DateTime.Now;
                List<SearchedPersonExperience> dbResult = new List<SearchedPersonExperience>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "تجربه کاری فرد موفقانه ثبت سیستم شد",
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

        //public async Task<IActionResult> OnPostDelete([FromBody] DeletePersonExperienceCommand command)
        //{
        //    try
        //    {
        //        string dbResult = string.Empty;
        //        dbResult = await Mediator.Send(command);

        //        return new JsonResult(new UIResult()
        //        {

        //            Data = new { list = dbResult },
        //            Status = UStatus,
        //            Text = "تجربه کاری انتخاب شده موفقانه حذف  شد",
        //            Description = string.Empty

        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new UIResult()
        //        {

        //            Data = null,
        //            Status = UStatus,
        //            Text = CustomMessages.InternalSystemException,
        //            Description = ex.Message + " \n StackTrace : " + ex.StackTrace

        //        });
        //    }
        //}

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonExperienceQuery searchQuery)
        {
            try
            {
                List<SearchedPersonExperience> dbResult = new List<SearchedPersonExperience>();
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