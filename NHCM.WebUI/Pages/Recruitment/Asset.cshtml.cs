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
    public class AssetModel : BasePage
    {
        public async Task OnGetAsync()
        {

            ListOfAssetType = new List<SelectListItem>();
            List<AssetType> assetTypes = new List<AssetType>();
            assetTypes = await Mediator.Send(new GetAssetTypeQuery() { ID = null });
            foreach (AssetType assetType in assetTypes)
                ListOfAssetType.Add(new SelectListItem() { Text = assetType.Name, Value = assetType.Id.ToString() });
        }


        public async Task<IActionResult> OnPostSave([FromBody] SavePersonAssetCommand command)
        {
            try
            {

                command.CreatedBy = 10;
                command.ModifiedBy = "Test";
                command.CreatedOn = DateTime.Now;
                command.ModifiedOn = DateTime.Now;

                List<SearchedPersonAsset> result = new List<SearchedPersonAsset>();
                result = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "سرمایه کارمند موفقانه ثبت سیستم شد",
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonAssetQuery query)
        {
            try
            {
                List<SearchedPersonAsset> dbResult = new List<SearchedPersonAsset>();
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


        public async Task<IActionResult> OnPostDelete([FromBody] DeletePersonAssetCommand command)
        {
            try
            {
                string dbResult = string.Empty;
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = " دارایی انتخاب شده موفقانه حذف  شد",
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