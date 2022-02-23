using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHCM.WebUI.Types;
using NHCM.Application.Recruitment.Commands;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.Application.Lookup.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using NHCM.Application.Document.Disk.FileManager;
using NHCM.Application.Document.Disk;
using NHCM.Application.Document.Disk.Cropper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using NHCM.Persistence.Infrastructure.Services;
using System.Linq;
using NHCM.Application.ProcessTracks.Commands;
using NHCM.Application.Lookup.Models;

namespace NHCM.WebUI.Pages.Recruitment
{

   
    public class PersonModel : BasePage
    {
        public string SubScreens { get; set; } = "";
        private string htmltemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>
                                    ";

        private readonly IConfiguration _configuration;
        private readonly ICurrentUser _currentUser;

        public PersonModel(IConfiguration configuration, ICurrentUser currentUser)
        {
            _configuration = configuration;
            _currentUser = currentUser;
        }



        public async Task OnGetAsync()
        {



            ListOfLocations = new List<SelectListItem>();
            List<Location> locations = new List<Location>();
            locations = await Mediator.Send(new GetLocationQuery() { ID = null });
            foreach (Location l in locations)
                ListOfLocations.Add(new SelectListItem(l.Dari, l.Id.ToString()));

            //Get blood groups
            ListOfBloodGroups = new List<SelectListItem>();
            List<BloodGroup> bloodGroups = new List<BloodGroup>();
            bloodGroups = await Mediator.Send(new GetBloodGroupQuery() { ID = null });
            foreach (BloodGroup bloodGroup in bloodGroups)
                ListOfBloodGroups.Add(new SelectListItem(bloodGroup.Name, bloodGroup.Id.ToString()));


            // Get Genders
            ListOfGenders = new List<SelectListItem>();
            List<Gender> Genders = new List<Gender>();
            Genders = await Mediator.Send(new GetGenderQuery() { ID = null });
            foreach (Gender gender in Genders)
                ListOfGenders.Add(new SelectListItem(gender.Dari, gender.ID.ToString()));

            // Get Marital Status
            ListOfMaritalStatus = new List<SelectListItem>();
            List<MaritalStatus> maritals = new List<MaritalStatus>();
            maritals = await Mediator.Send(new GetMaritalStatusQuery() { ID = null });
            foreach (MaritalStatus maritalStatus in maritals)
                ListOfMaritalStatus.Add(new SelectListItem(maritalStatus.Name, maritalStatus.Id.ToString()));


            ListOfReligions = new List<SelectListItem>();
            List<Religion> religions = new List<Religion>();
            religions = await Mediator.Send(new GetReligionQuery() { ID = null });
            foreach (Religion religion in religions)
                ListOfReligions.Add(new SelectListItem(religion.Name, religion.Id.ToString()));


            //Get Ethnicities
            ListOfEthnicities = new List<SelectListItem>();
            List<Ethnicity> ethnicities = new List<Ethnicity>();
            ethnicities = await Mediator.Send(new GetEthnicityQuery() { ID = null });
            foreach (Ethnicity ethnicity in ethnicities)
                ListOfEthnicities.Add(new SelectListItem(ethnicity.Name, ethnicity.Id.ToString()));


            ListOfDocumentTypes = new List<SelectListItem>();
            List<DocumentType> documentTypes = new List<DocumentType>();
            documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = 1, ID = null });
            foreach (DocumentType documentType in documentTypes)
                ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });



            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

            try
            {
                List<Screens> screens = new List<Screens>();
                screens = await Mediator.Send(new GetSubScreens() { ID = ScreenID });
                string listout = "";
                foreach (Screens s in screens)
                {
                    listout = listout + htmltemplate.Replace("$path", "dv_" + s.Path.Replace("/", "_")).Replace("$icon", s.Icon).Replace("$title", s.Title).Replace("$id", s.Id.ToString());
                }
                SubScreens = listout;
            }
            catch (Exception ex)
            {

            }

            int Screenid = Convert.ToInt32(EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]));
            List<SearchedProcess> Processes = await Mediator.Send(new GetProcess() { ScreenId = Screenid });
            if (Processes.Any())
            {
                HttpContext.Session.SetInt32("ModuleID", Processes.FirstOrDefault().ModuleId);
                HttpContext.Session.SetInt32("ProcessID", Processes.FirstOrDefault().Id);
            }
        }
        
        public async Task<IActionResult> OnPostSave([FromBody] CreatePersonCommand command)
        {
            try
            {
                  
                // Untill application of Identity
                command.ModifiedBy = "TEST USER";
                command.CreatedBy = 10;
                command.CreatedOn = DateTime.Now;

                command.ModuleID = HttpContext.Session.GetInt32("ModuleID").Value;
                command.ProcessID = HttpContext.Session.GetInt32("ProcessID").Value;
                command.OrganizationId = await _currentUser.GetUserOrganizationID();

                List<SearchedPersonModel> SaveResult = new List<SearchedPersonModel>();
                
                SaveResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "اطلاعات مستخدم موفقانه ثبت سیستم شد",
                    Description = string.Empty
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex)); 
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody]SearchPersonQuery searchQuery)
        {
         
            try
            {

                searchQuery.OrganizationId = await _currentUser.GetUserOrganizationID();

                List<SearchedPersonModel> SearchedResult = new List<SearchedPersonModel>();
                SearchedResult = await Mediator.Send(searchQuery);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = SearchedResult },
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

        public async Task<IActionResult> OnPostUpload([FromQuery]IFormFile img)
        {
            FileStorage _storage = new FileStorage();
            var extension = System.IO.Path.GetExtension(img.FileName);
            // check for a valid mediatype
            if (!img.ContentType.StartsWith("image/"))
            {
                return new JsonResult(new UIResult()
                {
                    Data = null,
                    Status = 0,
                    Text = "فارمت عکس درست نیست",
                    // Can be changed from app settings
                    Description = ""
                });
            }
            else
            {
                //var image = Image.FromStream(img.OpenReadStream(), true, true);
                string filename = await _storage.CreateAsync(img.OpenReadStream(), extension, _configuration["Photo"]);
                var result = new
                {
                    status = "success",
                    url = filename
                };
                return new JsonResult(result);
            }
        }


        public async Task<IActionResult> OnPostCrop([FromBody] CropRequest cropmodel)
        {
            FileStorage _storage = new FileStorage();
            string filename = await _storage.Crop(cropmodel, _configuration["Photo"]);

            var result = new object();
            try
            {
                result = new
                {
                    status = "success",
                    url = filename
                };
            }
            catch (Exception e)
            {
                result = new
                {
                    status = "fail",
                    url = "",
                };
            }
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostDownload([FromBody] UploadedFile file)
        {
            FileStorage _storage = new FileStorage();
            var filepath = _configuration["Photo"] + file.Name;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file.Name);
        }
    }
}