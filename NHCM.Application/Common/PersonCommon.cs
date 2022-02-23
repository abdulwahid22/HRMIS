using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NHCM.Persistence;
using NHCM.Application.Recruitment.Models;
using System.Threading.Tasks;
using NHCM.Application.Recruitment.Queries;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Newtonsoft.Json;
using NHCM.Persistence.Infrastructure.Services;

namespace NHCM.Application.Common
{
    public class PersonCommon
    {
        private readonly HCMContext _context;


        public PersonCommon(HCMContext context)
        {
            _context = context;

        }

        public string ConvertJSONToString(String JSON, String Type)
        {
            if (!string.IsNullOrEmpty(JSON))
            {
                dynamic item = JsonConvert.DeserializeObject(JSON);
                string result = Type + " | ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["S"]))) result = result + "نمبر سند هویت: " + item["S"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["J"]))) result = result + "جلد: " + item["J"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["P"]))) result = result + "صفحه: " + item["P"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["N"]))) result = result + "نمبر ثبت: " + item["N"] + "، ";

                int last = result.LastIndexOf('،');
                return result.Remove(last);
            }
            else
            {
                return null;
            }

        }


        public async Task<List<SearchedPersonModel>> SearchPerson(SearchPersonQuery request)
        {
            List<SearchedPersonModel> searchResult = new List<SearchedPersonModel>();
            // If specific person is required. Searched based on Id or HrCode
            if (request.Id != null)
            {
                searchResult = await (from p in _context.Person
                                      join g in _context.Gender on p.GenderId equals g.ID into pg
                                      from resultPG in pg.DefaultIfEmpty()
                                      join b in _context.BloodGroup on p.BloodGroupId equals b.Id into pb
                                      from resultPB in pb.DefaultIfEmpty()
                                      join e in _context.Ethnicity on p.EthnicityId equals e.Id into pe
                                      from resultPE in pe.DefaultIfEmpty()
                                      join r in _context.Religion on p.ReligionId equals r.Id into pr
                                      from resultPR in pr.DefaultIfEmpty()
                                      join l in _context.Location on p.BirthLocationId equals l.Id into pl
                                      from resultPL in pl.DefaultIfEmpty()
                                      join m in _context.MaritalStatus on p.MaritalStatusId equals m.Id into pm
                                      from resultPM in pm.DefaultIfEmpty()
                                      join dt in _context.DocumentTypes on p.DocumentTypeId equals dt.Id into pdt
                                      from resultPDT in pdt.DefaultIfEmpty()
                                      where p.Id == request.Id
                                      select new SearchedPersonModel
                                      {
                                          FirstName = p.FirstName,
                                          LastName = p.LastName,
                                          FirstNameEng = p.FirstNameEng,
                                          LastNameEng = p.LastNameEng,
                                          FatherName = p.FatherName,
                                          FatherNameEng = p.FatherNameEng,
                                          GenderId = p.GenderId,
                                          DateOfBirth = p.DateOfBirth,
                                          GrandFatherName = p.GrandFatherName,
                                          GrandFatherNameEng = p.GrandFatherNameEng,
                                          Id = p.Id,
                                          DocumentTypeId = p.DocumentTypeId,
                                          Nid = p.Nid,
                                          PhotoPath = p.PhotoPath,
                                          Hrcode = p.Hrcode,
                                          BirthLocationId = p.BirthLocationId,
                                          MaritalStatusId = p.MaritalStatusId,
                                          ReligionId = p.ReligionId,
                                          EthnicityId = p.EthnicityId,
                                          Comments = p.Comments,
                                          BloodGroupId = p.BloodGroupId,
                                          GenderText = resultPG.Dari,
                                          BloodGroupText = resultPB.Name,
                                          EthnicityText = resultPE.Name,
                                          ReligionText = resultPR.Name,
                                          BirthLocationText = resultPL.Name,
                                          MaritalStatusText = resultPM.Name,
                                          DocumentTypeText = resultPDT.Name,
                                          NIDText = ConvertJSONToString(p.Nid, resultPDT.Name) ?? "درج نگردیده",
                                          DoBText = PersianLibrary.PersianDate.GetFormatedString(p.DateOfBirth)
                                      }).OrderBy(x => x.Id).ToListAsync();
            }
            // Else search based on search terms
            else if (request.OrganizationId != null)
            {
                searchResult = await (from p in _context.Person
                                      join g in _context.Gender on p.GenderId equals g.ID into pg
                                      from resultPG in pg.DefaultIfEmpty()
                                      join b in _context.BloodGroup on p.BloodGroupId equals b.Id into pb
                                      from resultPB in pb.DefaultIfEmpty()
                                      join e in _context.Ethnicity on p.EthnicityId equals e.Id into pe
                                      from resultPE in pe.DefaultIfEmpty()
                                      join r in _context.Religion on p.ReligionId equals r.Id into pr
                                      from resultPR in pr.DefaultIfEmpty()
                                      join l in _context.Location on p.BirthLocationId equals l.Id into pl
                                      from resultPL in pl.DefaultIfEmpty()
                                      join m in _context.MaritalStatus on p.MaritalStatusId equals m.Id into pm
                                      from resultPM in pm.DefaultIfEmpty()
                                      join dt in _context.DocumentTypes on p.DocumentTypeId equals dt.Id into pdt
                                      from resultPDT in pdt.DefaultIfEmpty()
                                      where (p.FirstName.Contains(request.FirstName) || string.IsNullOrEmpty(request.FirstName))
                                            && (p.LastName.Contains(request.LastName) || string.IsNullOrEmpty(request.LastName))
                                            && (p.FatherName.Contains(request.FatherName) || string.IsNullOrEmpty(request.FatherName))
                                            && (p.GrandFatherName.Contains(request.GrandFatherName) || string.IsNullOrEmpty(request.GrandFatherName))
                                            && (p.FirstNameEng.Contains(request.FirstNameEng) || string.IsNullOrEmpty(request.FirstNameEng))
                                            && (p.LastNameEng.Contains(request.LastNameEng) || string.IsNullOrEmpty(request.LastNameEng))
                                            && (p.GrandFatherNameEng.Contains(request.GrandFatherNameEng) || string.IsNullOrEmpty(request.GrandFatherNameEng))
                                            && (p.OrganizationId == request.OrganizationId)
                                      select new SearchedPersonModel
                                      {
                                          FirstName = p.FirstName,
                                          LastName = p.LastName,
                                          FirstNameEng = p.FirstNameEng,
                                          LastNameEng = p.LastNameEng,
                                          FatherName = p.FatherName,
                                          FatherNameEng = p.FatherNameEng,
                                          GenderId = p.GenderId,
                                          DateOfBirth = p.DateOfBirth,
                                          GrandFatherName = p.GrandFatherName,
                                          GrandFatherNameEng = p.GrandFatherNameEng,
                                          Id = p.Id,
                                          DocumentTypeId = p.DocumentTypeId,
                                          Hrcode = p.Hrcode,
                                          BirthLocationId = p.BirthLocationId,
                                          MaritalStatusId = p.MaritalStatusId,
                                          ReligionId = p.ReligionId,
                                          EthnicityId = p.EthnicityId,
                                          Comments = p.Comments,
                                          BloodGroupId = p.BloodGroupId,
                                          Nid = p.Nid,
                                          PhotoPath = p.PhotoPath,
                                          GenderText = resultPG.Dari,
                                          BloodGroupText = resultPB.Name,
                                          EthnicityText = resultPE.Name,
                                          ReligionText = resultPR.Name,
                                          BirthLocationText = resultPL.Name,
                                          MaritalStatusText = resultPM.Name,
                                          DocumentTypeText = resultPDT.Name,
                                          NIDText = ConvertJSONToString(p.Nid, resultPDT.Name) ?? "درج نگردیده",
                                          DoBText = PersianLibrary.PersianDate.GetFormatedString(p.DateOfBirth)
                                      }).OrderBy(x => x.Id).ToListAsync();
            }
            else
            {
                searchResult = await (from p in _context.Person
                                      join g in _context.Gender on p.GenderId equals g.ID into pg
                                      from resultPG in pg.DefaultIfEmpty()
                                      join b in _context.BloodGroup on p.BloodGroupId equals b.Id into pb
                                      from resultPB in pb.DefaultIfEmpty()
                                      join e in _context.Ethnicity on p.EthnicityId equals e.Id into pe
                                      from resultPE in pe.DefaultIfEmpty()
                                      join r in _context.Religion on p.ReligionId equals r.Id into pr
                                      from resultPR in pr.DefaultIfEmpty()
                                      join l in _context.Location on p.BirthLocationId equals l.Id into pl
                                      from resultPL in pl.DefaultIfEmpty()
                                      join m in _context.MaritalStatus on p.MaritalStatusId equals m.Id into pm
                                      from resultPM in pm.DefaultIfEmpty()
                                      join dt in _context.DocumentTypes on p.DocumentTypeId equals dt.Id into pdt
                                      from resultPDT in pdt.DefaultIfEmpty()
                                      select new SearchedPersonModel
                                      {
                                          FirstName = p.FirstName,
                                          LastName = p.LastName,
                                          FirstNameEng = p.FirstNameEng,
                                          LastNameEng = p.LastNameEng,
                                          FatherName = p.FatherName,
                                          FatherNameEng = p.FatherNameEng,
                                          GenderId = p.GenderId,
                                          DateOfBirth = p.DateOfBirth,
                                          GrandFatherName = p.GrandFatherName,
                                          GrandFatherNameEng = p.GrandFatherNameEng,
                                          Id = p.Id,
                                          DocumentTypeId = p.DocumentTypeId,
                                          Hrcode = p.Hrcode,
                                          BirthLocationId = p.BirthLocationId,
                                          MaritalStatusId = p.MaritalStatusId,
                                          ReligionId = p.ReligionId,
                                          EthnicityId = p.EthnicityId,
                                          Comments = p.Comments,
                                          BloodGroupId = p.BloodGroupId,
                                          Nid = p.Nid,
                                          PhotoPath = p.PhotoPath,
                                          GenderText = resultPG.Dari,
                                          BloodGroupText = resultPB.Name,
                                          EthnicityText = resultPE.Name,
                                          ReligionText = resultPR.Name,
                                          BirthLocationText = resultPL.Name,
                                          MaritalStatusText = resultPM.Name,
                                          DocumentTypeText = resultPDT.Name,
                                          NIDText = ConvertJSONToString(p.Nid, resultPDT.Name) ?? "درج نگردیده",
                                          DoBText = PersianLibrary.PersianDate.GetFormatedString(p.DateOfBirth)
                                      }).OrderBy(x => x.Id).Take((request.NoOfRecords == 0) ? 100 : request.NoOfRecords).ToListAsync();
            }
            return searchResult;
        }
        public async Task<List<SearchedPersonRelative>> SearchPersonRelative(SearchPersonRelativeQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonRelative> result = new List<SearchedPersonRelative>();
            if (request.Id != null)
            {
                using (_context)
                {
                    result = await (from r in _context.Relative

                                    join relationship in _context.Relationship on r.RelationShipId equals relationship.Id into rR
                                    from resultRr in rR.DefaultIfEmpty()
                                    join l in _context.Location on r.LocationId equals l.Id into rL
                                    from resultrL in rL.DefaultIfEmpty()

                                    where r.Id == request.Id
                                    select new SearchedPersonRelative
                                    {

                                        Id = r.Id,
                                        FatherName = r.FatherName,
                                        FirstName = r.FirstName,
                                        PersonId = r.PersonId,
                                        LastName = r.LastName,
                                        GrandFatherName = r.GrandFatherName,
                                        RelationShipId = r.RelationShipId,
                                        NidNo = r.NidNo,
                                        Profession = r.Profession,
                                        Address = r.Address,
                                        ContactInfo = r.ContactInfo,
                                        EmailAddress = r.EmailAddress,
                                        Village = r.Village,
                                        LocationId = r.LocationId,
                                        Remark = r.Remark,
                                        RelationShipIdText = resultRr.Name,
                                        LocationText = resultrL.Dari


                                    }).ToListAsync(cancellationToken);
                }
            }

            else if (request.PersonId != null)
            {
                using (_context)
                {
                    result = await (from r in _context.Relative
                                    join relationship in _context.Relationship on r.RelationShipId equals relationship.Id into rR
                                    from resultRr in rR.DefaultIfEmpty()
                                    join l in _context.Location on r.LocationId equals l.Id into rL
                                    from resultrL in rL.DefaultIfEmpty()
                                    where r.PersonId == request.PersonId
                                    //where (r.FirstName.Contains(request.FirstName) || string.IsNullOrEmpty(request.FirstName))

                                    //       && (r.FatherName.Contains(request.FatherName) || string.IsNullOrEmpty(request.FatherName))
                                    //       && (r.GrandFatherName.Contains(request.GrandFatherName) || string.IsNullOrEmpty(request.GrandFatherName))
                                    //       && (r.EmailAddress.Contains(request.EmailAddress) || string.IsNullOrEmpty(request.EmailAddress))
                                    //       && ( r.ContactInfo.Contains(request.ContactInfo) || string.IsNullOrEmpty(request.ContactInfo) )
                                    //       && ( r.LocationId == request.LocationId || request.LocationId ==null || request.LocationId == 0.0m)
                                    //       && ( r.RelationShipId == request.RelationShipId || request.RelationShipId == null || request.RelationShipId ==0.0)

                                    select new SearchedPersonRelative
                                    {

                                        Id = r.Id,
                                        FatherName = r.FatherName,
                                        FirstName = r.FirstName,
                                        PersonId = r.PersonId,
                                        LastName = r.LastName,
                                        GrandFatherName = r.GrandFatherName,
                                        RelationShipId = r.RelationShipId,
                                        NidNo = r.NidNo,
                                        Profession = r.Profession,
                                        Address = r.Address,
                                        ContactInfo = r.ContactInfo,
                                        EmailAddress = r.EmailAddress,
                                        Village = r.Village,
                                        LocationId = r.LocationId,
                                        Remark = r.Remark,
                                        RelationShipIdText = resultRr.Name,
                                        LocationText = resultrL.Dari


                                    }).ToListAsync(cancellationToken);
                }
            }


            return result;
        }


        public async Task<List<SearchedPersonLanguage>> SearchPersonLanguages(SearchPersonLanguageQuery request, CancellationToken cancellationToken)
        {
            List<SearchedPersonLanguage> result = new List<SearchedPersonLanguage>();


            if (request.Id != null)
            {
                result = await (from pl in _context.PersonLanguage
                                join l in _context.Language on pl.LanguageId equals l.Id into plL
                                from resultplL in plL.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.ReadingExpertise equals ex.Id into plRex
                                from resultplRex in plRex.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.WritingExpertise equals ex.Id into plWex
                                from resultplWex in plWex.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.ReadingExpertise equals ex.Id into plUex
                                from resultplUex in plUex.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.ReadingExpertise equals ex.Id into plSpeakingEx
                                from resultplplSpeakingEx in plSpeakingEx.DefaultIfEmpty()



                                where pl.Id == request.Id
                                select new SearchedPersonLanguage
                                {

                                    Id = pl.Id,
                                    PersonId = pl.PersonId,
                                    LanguageId = pl.LanguageId,
                                    ReadingExpertise = pl.ReadingExpertise,
                                    UnderstandingExpertise = pl.UnderstandingExpertise,
                                    WritingExpertise = pl.WritingExpertise,
                                    SpeakingExpertise = pl.SpeakingExpertise,

                                    LanguageText = resultplL.Name,
                                    ReadingExpertiseText = resultplRex.Name,
                                    UnderstandingExpertiseText = resultplUex.Name,
                                    SpeakingExpertiseText = resultplplSpeakingEx.Name,
                                    WritingExpertiseText = resultplWex.Name


                                }).ToListAsync();
            }


            else if (request.PersonId != null)
            {
                result = await (from pl in _context.PersonLanguage
                                join l in _context.Language on pl.LanguageId equals l.Id into plL
                                from resultplL in plL.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.ReadingExpertise equals ex.Id into plRex
                                from resultplRex in plRex.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.WritingExpertise equals ex.Id into plWex
                                from resultplWex in plWex.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.ReadingExpertise equals ex.Id into plUex
                                from resultplUex in plUex.DefaultIfEmpty()

                                join ex in _context.Expertise on pl.ReadingExpertise equals ex.Id into plSpeakingEx
                                from resultplplSpeakingEx in plSpeakingEx.DefaultIfEmpty()



                                where pl.PersonId == request.PersonId
                                select new SearchedPersonLanguage
                                {

                                    Id = pl.Id,
                                    PersonId = pl.PersonId,
                                    LanguageId = pl.LanguageId,
                                    ReadingExpertise = pl.ReadingExpertise,
                                    UnderstandingExpertise = pl.UnderstandingExpertise,
                                    WritingExpertise = pl.WritingExpertise,
                                    SpeakingExpertise = pl.SpeakingExpertise,

                                    LanguageText = resultplL.Name,
                                    ReadingExpertiseText = resultplRex.Name,
                                    UnderstandingExpertiseText = resultplUex.Name,
                                    SpeakingExpertiseText = resultplplSpeakingEx.Name,
                                    WritingExpertiseText = resultplWex.Name


                                }).ToListAsync();
            }






            return result;
        }



    }
}
