using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using NHCM.Domain;
using NHCM.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using NHCM.Persistence.Extensions;
using NHCM.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Infrastructure.Exceptions;
using NHCM.Application.Recruitment.Queries;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Common;
using NHCM.Persistence.Infrastructure.Services;

namespace NHCM.Application.Recruitment.Commands
{
    public class CreatePersonCommand : IRequest<List<SearchedPersonModel>>
    {
        public string FatherName { get; set; }
        public decimal? Id { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string Hrcode { get; set; }
        public string LastName { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public string GrandFatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public short? MaritalStatusId { get; set; }
        public int? EthnicityId { get; set; }
        public short? ReligionId { get; set; }
        public string Comments { get; set; }
        public int? StatusId { get; set; }
        public string Remark { get; set; }
        public int? BloodGroupId { get; set; }

        public int? DocumentTypeId { get; set; }
        public string PhotoPath { get; set; }
        public string Nid { get; set; }
        public int? OrganizationId { get; set; }

        public int ModuleID { get; set; }
        public int ProcessID { get; set; }

    }



    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand,List<SearchedPersonModel>>
    {

        private readonly HCMContext _context;
        private readonly IMediator _mediator;
        private readonly PersonCommon _personCommon;
        private readonly ICurrentUser _currentUser;
        public CreatePersonCommandHandler(HCMContext context, IMediator mediator, ICurrentUser currentUser)
        {
            _context = context;
            _mediator = mediator;
            _personCommon = new PersonCommon(_context);
            _currentUser = currentUser;

        }
        public async Task<List<SearchedPersonModel>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            List<SearchedPersonModel> result = new List<SearchedPersonModel>();

            // Save
            if (request.Id == null || request.Id == default(decimal))
            {

                int CurrentUserId = await _currentUser.GetUserId();

                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        #region BuildHrCode
                        StringBuilder PrefixBuilder = new StringBuilder(string.Empty);
                        StringBuilder HrCodeBuilder = new StringBuilder(string.Empty);

                        // Build Prefix
                        PrefixBuilder.Append(("00" + request.BirthLocationId.ToString()).Right(2));
                        PrefixBuilder.Append(("00" + Convert.ToDateTime(request.DateOfBirth).Month.ToString()).Right(2));
                        PrefixBuilder.Append(("0000" + Convert.ToDateTime(request.DateOfBirth).Year.ToString()).Right(4));
                        PrefixBuilder.Append(("00" + Convert.ToDateTime(request.CreatedOn).Day.ToString()).Right(2));
                        PrefixBuilder.Append(("00" + Convert.ToDateTime(request.CreatedOn).Month.ToString()).Right(2));
                        PrefixBuilder.Append(Convert.ToDateTime(request.CreatedOn).Year.ToString().Right(2));

                        //Build Suffix
                        //Get Current Suffix where its prefix is equal to PrefixBuilder.
                        int? Suffix;
                        int? CurrentSuffix = await _context.Person.Where(p => p.PreFix == PrefixBuilder.ToString()).MaxAsync(s => s.Suffix);
                        if (CurrentSuffix is null) CurrentSuffix = 1;
                        Suffix = CurrentSuffix + 1;

                        // Build HR Code
                        HrCodeBuilder.Append(PrefixBuilder.ToString());
                        HrCodeBuilder.Append(("000" + Suffix.ToString()).Right(3));
                        #endregion BuildHrCode
                        #region SaveAndReturnResult
                        // Construct Person Object
                        Person person = new Person()
                        {
                            FirstName = request.FirstName.Trim(),
                            FirstNameEng = request.FirstNameEng.Trim(),
                            LastName = request.LastName.Trim(),
                            FatherName = request.FatherName,
                            FatherNameEng = request.FatherNameEng,
                            GrandFatherName = request.GrandFatherName,
                            GrandFatherNameEng = request.GrandFatherNameEng,
                            LastNameEng = request.LastNameEng.Trim(),
                            PreFix = PrefixBuilder.ToString(),
                            Suffix = Suffix,
                            Hrcode = HrCodeBuilder.ToString(),
                            DateOfBirth = request.DateOfBirth,
                            BirthLocationId = request.BirthLocationId,
                            GenderId = request.GenderId,
                            MaritalStatusId = request.MaritalStatusId,
                            EthnicityId = request.EthnicityId,
                            ReligionId = request.ReligionId,
                            Comments = request.Comments,
                            StatusId = request.StatusId,
                            Remark = request.Remark,
                            BloodGroupId = request.BloodGroupId,
                            ModifiedBy = request.ModifiedBy,
                            CreatedBy = request.CreatedBy,
                            CreatedOn = request.CreatedOn,

                            Nid = request.Nid,
                            PhotoPath = request.PhotoPath,
                            DocumentTypeId = request.DocumentTypeId,
                            OrganizationId = request.OrganizationId
                        };
                        _context.Person.Add(person);
                        await _context.SaveChangesAsync(CurrentUserId, cancellationToken);

                        ProcessTracking PT = new ProcessTracking()
                        {
                            // CHANGE: Don't convert person.Id to integer, instead make the RecordId to include different types
                            RecordId = Convert.ToInt32(person.Id),
                            ProcessId = (Int16)request.ProcessID,
                            StatusId = 5, // In Process
                            ModuleId = request.ModuleID,
                            ReferedProcessId = 1,
                            CreatedOn = DateTime.Now
                        };
                        _context.ProcessTracking.Add(PT);

                        await _context.SaveChangesAsync(CurrentUserId,  cancellationToken);

                        result = await _personCommon.SearchPerson(new SearchPersonQuery() { Id = person.Id });


                        #endregion SaveAndReturnResult

                        transaction.Commit();
                    }
                    catch (Exception  ex)
                    {
                        transaction.Rollback();
                        throw new Exception();
                    }
                }

                return result;

            }
            // Update
            else
            {

                Person UpdateablePerson = (from p in _context.Person
                                           where p.Id == request.Id
                                           select p).First();

                UpdateablePerson.FirstName = request.FirstName;
                UpdateablePerson.LastName = request.LastName;
                UpdateablePerson.FatherName = request.FatherName;
                UpdateablePerson.FatherNameEng = request.FatherNameEng;
                UpdateablePerson.GrandFatherName = request.GrandFatherName;
                UpdateablePerson.FirstNameEng = request.FirstNameEng;
                UpdateablePerson.LastNameEng = request.LastNameEng;
                UpdateablePerson.GrandFatherNameEng = request.GrandFatherNameEng;
                UpdateablePerson.BirthLocationId = request.BirthLocationId;
                UpdateablePerson.GenderId = request.GenderId;
                UpdateablePerson.MaritalStatusId = request.MaritalStatusId;
                UpdateablePerson.EthnicityId = request.EthnicityId;
                UpdateablePerson.ReligionId = request.ReligionId;
                UpdateablePerson.Comments = request.Comments;
                UpdateablePerson.BloodGroupId = request.BloodGroupId;
                UpdateablePerson.Nid = request.Nid;
                UpdateablePerson.PhotoPath = request.PhotoPath;
                UpdateablePerson.DocumentTypeId = request.DocumentTypeId;
                UpdateablePerson.OrganizationId = request.OrganizationId;

                await _context.SaveChangesAsync();

                result = await _personCommon.SearchPerson(new SearchPersonQuery() { Id = UpdateablePerson.Id });

                return result;

            }
        }
    }
}
