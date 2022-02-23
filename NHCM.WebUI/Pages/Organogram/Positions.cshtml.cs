using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Organogram.Commands;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Organogram
{
  //  [Authorize(Policy = "OrganizationTahskil")]

    public class PositionsModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfStatus = new List<SelectListItem>();
            List<Status> statuses = new List<Status>();
            statuses = await Mediator.Send(new GetStatusQuery() { category = "PS" });
            foreach (Status status in statuses)
                ListOfStatus.Add(new SelectListItem() { Text = status.Dari, Value = status.Id.ToString() });


            // Page Specific Lookups
            ListOfPlanType = new List<SelectListItem>();
            List<PlanType> plantype = new List<PlanType>();
            plantype = await Mediator.Send(new GetPlanTypeQuery() { Id = null });
            foreach (PlanType planT in plantype)
                ListOfPlanType.Add(new SelectListItem() { Text = planT.Name, Value = planT.Id.ToString() });

            ListOfPositionType = new List<SelectListItem>();
            List<SearchedOrgPosition> PositionType = new List<SearchedOrgPosition>();
            PositionType = await Mediator.Send(new SearchOrgPositionQuery() { });
            foreach (SearchedOrgPosition po in PositionType)
                ListOfPositionType.Add(new SelectListItem() { Text = po.PositionTypeText + " | " + po.RankText, Value = po.Id.ToString() });

            ListOfWorkAreas = new List<SelectListItem>();
            List<WorkArea> WorkAreas = new List<WorkArea>();
            WorkAreas = await Mediator.Send(new GetWorkAreaQuery() { });
            foreach (WorkArea wa in WorkAreas)
                ListOfWorkAreas.Add(new SelectListItem() { Text = wa.Title, Value = wa.Id.ToString() });


            ListOfSalaryType = new List<SelectListItem>();
            List<SalaryType> salaryTypes = new List<SalaryType>();
            salaryTypes = await Mediator.Send(new GetSalaryTypeQuery() { Id = null });
            foreach (SalaryType salary in salaryTypes)
                ListOfSalaryType.Add(new SelectListItem() { Text = salary.Dari, Value = salary.Id.ToString() });

            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));
        }


        public async Task<IActionResult> OnPostSave([FromBody]SavePositionCommand command)
        {
            try
            {

                List<SearchedPosition> dbResult = new List<SearchedPosition>();
                dbResult = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "بست موفقانه ثبت سیستم شد",
                    Description = string.Empty

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }

        }

        public async Task<IActionResult> OnPostRemove([FromBody] DeletePositionCommand command)
        {
            try
            {
                List<SearchedPosition> theParentOfDeletedPosition = new List<SearchedPosition>();
                theParentOfDeletedPosition = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = theParentOfDeletedPosition },
                    Status = UIStatus.Success,
                    Text = "بست انتخاب شده موفقانه حذف گردید",
                    Description = string.Empty

                });

            }
            catch(Exception ex)
            {



                return new JsonResult(new UIResult()
                {
                    Data = new { },
                    Status = UIStatus.Failure,
                    Text = CustomMessages.StateExceptionTitle(ex),
                    Description =CustomMessages.DescribeException(ex)
                });

            }
        }

        public async Task<IActionResult> OnPostPositions([FromBody] SearchPositionQuery command)
        {

            try
            {
                List<SearchedPosition> presult = new List<SearchedPosition>();
                presult = await Mediator.Send(command);

                List<object> result = new List<object>();
                List<SearchedOrgPosition> PositionType = new List<SearchedOrgPosition>();
                PositionType = await Mediator.Send(new SearchOrgPositionQuery() { Id = presult.FirstOrDefault().PositionTypeId, Children = true });

                foreach (SearchedOrgPosition po in PositionType)
                    result.Add(new { Text = po.PositionTypeText + " | " + po.RankText, ID = po.Id.ToString() });


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
                return new JsonResult(new UIResult()
                {
                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message
                });
            }
        }


        public async Task<IActionResult> OnPostSearch([FromBody] SearchPositionQuery command)
        {
            try
            {
                List<SearchedPosition> result = new List<SearchedPosition>();
                result = await Mediator.Send(command);
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