using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using NHCM.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace NHCM.WebUI.Types
{
    public abstract class BasePage : PageModel
    {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        //private IConfiguration _configuration;
        //public BasePage(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public List<SelectListItem> ListOfMaritalStatus;
        public List<SelectListItem> ListOfGenders;
        public List<SelectListItem> ListOfLocations;
        public List<SelectListItem> ListOfBloodGroups;
        public List<SelectListItem> ListOfEthnicities;
        public List<SelectListItem> ListOfReligions;
        public List<SelectListItem> ListOfLanguages;
        public List<SelectListItem> ListOfJobStatus;
        public List<SelectListItem> ListOfResult = new List<SelectListItem>();
        public List<SelectListItem> ListOfRanks;
        public List<SelectListItem> ListOfOrganizationType;
        public List<SelectListItem> ListOfCertification;
        public List<SelectListItem> ListOfExperienceType;
        public List<SelectListItem> ListOfRelationShip;
        public List<SelectListItem> ListOfExpertise;
        public List<SelectListItem> ListOfSkillType;
        public List<SelectListItem> ListOfDistricts;
        public List<SelectListItem> ListOfEducationLevels;
        public List<SelectListItem> ListOfStatus;
        public List<SelectListItem> ListOfAssetType;
        public List<SelectListItem> ListOfReferenceType;
        public List<SelectListItem> ListOfDocumentTypes;
        public List<SelectListItem> ListOfDocumentTypesD;
        public List<SelectListItem> ListOfPublicationType;
        public List<SelectListItem> ListOfOrganization;
        public List<SelectListItem> ListOfOrgUnit = new List<SelectListItem>();
        public List<SelectListItem> ListOfOrgUnitType;
        public List<SelectListItem> ListOfSalaryType; 
        public List<SelectListItem> ListOfPlanType;
        public List<SelectListItem> ListOfPositionType;
        public List<EducationLevel> ListOfOrganoGram = new List<EducationLevel>();
        public List<SelectListItem> ListOfMilitaryServiceType;
        public List<SelectListItem> ListOfEventType;
        public List<SelectListItem> ListOfPerson;
        public List<SelectListItem> ListOfPosition;
        public List<SelectListItem> ListOfProcesses = new List<SelectListItem>();
        public List<SelectListItem> ListOfPersianYears = new List<SelectListItem>();
        public List<SelectListItem> ListOfWorkAreas = new List<SelectListItem>();
        public List<SelectListItem> ListOfReportTo; 
        public List<SelectListItem> ListOfDepartment;

        public List<SelectListItem> ListOfEmployees;
        public List<SelectListItem> ListOfZones;
        public List<SelectListItem> ListOfYears;
        public List<SelectListItem> ListOfMonths;


    }
}
