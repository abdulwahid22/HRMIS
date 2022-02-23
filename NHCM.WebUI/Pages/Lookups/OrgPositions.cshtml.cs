using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Commands;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Lookups
{
    public class OrgPositionsModel : BasePage
    {
        public async Task OnGetAsync()
        { 
            ListOfPositionType = new List<SelectListItem>();
            List<PositionType> positionTypes = new List<PositionType>();
            positionTypes = await Mediator.Send(new GetOrgPositionTypeQuery() { ID = null });
            foreach (PositionType po in positionTypes)
                ListOfPositionType.Add(new SelectListItem() { Text = po.Name, Value = po.Id.ToString() }); 


            ListOfOrgUnitType = new List<SelectListItem>();
            List<OrgUnitType> orgUnitTypes = new List<OrgUnitType>();
            orgUnitTypes = await Mediator.Send(new GetOrgUnitTypeQuery() { ID = null });
            foreach (OrgUnitType OUT in orgUnitTypes)
            {
                ListOfOrgUnitType.Add(new SelectListItem() { Text = OUT.Name, Value = OUT.Id.ToString() });
            }

            ListOfReportTo = new List<SelectListItem>();
            List<PositionType> repToPositiont = new List<PositionType>();
            repToPositiont = await Mediator.Send(new GetReportToQuery() { ID = null});
            foreach (PositionType opst in repToPositiont)
            {
                ListOfReportTo.Add(new SelectListItem() { Text = opst.Name, Value = opst.Id.ToString() });
            }


            ListOfRanks = new List<SelectListItem>();
            List<Rank> ranks = new List<Rank>();
            ranks = await Mediator.Send(new GetRankQuery() { ID = null });
            foreach (Rank rank in ranks)
                ListOfRanks.Add(new SelectListItem() { Text = rank.Name, Value = rank.Id.ToString() });



        }

        public async Task<IActionResult> OnPostSave([FromBody]SaveOrgPositionCommand command)
        { 
            try
            { 
                List<SearchedOrgPositionModel> dbResult = new List<SearchedOrgPositionModel>();
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
         
        public async Task<IActionResult> OnPostSearch([FromBody] SearchOrgPosition_Query query)
        {
            try
            {
                List<SearchedOrgPositionModel> result = new List<SearchedOrgPositionModel>();

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