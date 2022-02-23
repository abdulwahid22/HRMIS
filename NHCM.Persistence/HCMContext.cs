using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using NHCM.Domain.Entities;
using NHCM.Persistence.Infrastructure;
using System.Linq;
using NHCM.Persistence.Infrastructure.Services;
using NHCM.Domain.ReportEntities;
using NHCM.Domain.ViewsEntities;

namespace NHCM.Persistence
{
    public partial class HCMContext : DbContext
    {
        public HCMContext()
        {
        }
        public HCMContext(DbContextOptions<HCMContext> options)
            : base(options)
        {
        }

        #region DbSets
        public virtual DbSet<Audit> Audits { get; set; }

        public virtual DbSet<OperationType> OperationType { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AssetType> AssetType { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<MilitaryService> MilitaryService { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<PersonAsset> PersonAsset { get; set; }
        public virtual DbSet<PersonLanguage> PersonLanguage { get; set; }
        public virtual DbSet<PersonSkill> PersonSkill { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Religion> Religion { get; set; }
        public virtual DbSet<Ethnicity> Ethnicity { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<BloodGroup> BloodGroup { get; set; }
        public virtual DbSet<Expertise> Expertise { get; set; }
        public virtual DbSet<Relationship> Relationship { get; set; }
        public virtual DbSet<Relative> Relative { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Institute> Institute { get; set; }
        public virtual DbSet<ExperienceType> ExperienceType { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<EducationLevel> EducationLevel { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Experience> Experience { get; set; }
        public virtual DbSet<SkillType> SkillType { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<OrganizationType> OrganizationType { get; set; }
        public virtual DbSet<Certification> Certification { get; set; }
        public virtual DbSet<ReferenceType> ReferenceType { get; set; }
        public virtual DbSet<Reference> Reference { get; set; }
        public virtual DbSet<Screens> Screens { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<HealthReport> HealthReport { get; set; }
        public virtual DbSet<Travel> Travel { get; set; }
        public virtual DbSet<PublicationType> PublicationType { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<FolderPath> FolderPath { get; set; }
        public virtual DbSet<Documents> Document { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Selection> Selection { get; set; }
        public virtual DbSet<Selection> Selections { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<ProcessTracking> ProcessTracking { get; set; }
        public virtual DbSet<OrgUnit> OrgUnit { get; set; }
        public virtual DbSet<OrganoGram> OrganoGram { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Characteristic> Characteristic { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionType> PositionType { get; set; }
        public virtual DbSet<PositionChange> PositionChange { get; set; }
        public virtual DbSet<PlanType> PlanType { get; set; }
        public virtual DbSet<SalaryType> SalaryType { get; set; }
        public virtual DbSet<MilitaryServiceType> MilitaryServiceType { get; set; }
        public virtual DbSet<WorkArea> WorkArea { get; set; }
        public virtual DbSet<OrgPosition> OrgPosition { get; set; }
        public virtual DbSet<OrgUnitType> OrgUnitType { get; set; }
        public virtual DbSet<ProcessConnection> ProcessConnection { get; set; }
        public virtual DbSet<DailyLog> DailyLog { get; set; }
        public virtual DbSet<Zones> Zones { get; set; } 
        public virtual DbSet<ZoneEmployees> ZoneEmployees { get; set; }
        public virtual DbSet<IdentityCard> IdentityCard { get; set; }
        public virtual DbQuery<carddetails> Carddetails { get; set; }

        public virtual DbQuery<ZoneCardDetails> ZoneCardDetails { get; set; }
        ///// Report Dbset
        public virtual DbQuery<StatisticalReport> StatisticalReports { get; set; }
        public virtual DbQuery<AttendanceReport> AttendanceReports { get; set; }
        public virtual DbQuery<vUserInfo> VUserInfos { get; set; }
        #endregion DbSets


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {  
                optionsBuilder.UseNpgsql("Server=localhost; Database =HCM; Username=postgres; Password=P@33word");
            }
        }




        #region AuditionSetting

     //   private CurrentUser currentUser;
        public  async Task<int> SaveChangesAsync(int UserId, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<AuditEntry> AuditEntries = new List<AuditEntry>();

            AuditEntries =   OnBeforeSaveChanges(UserId);
            var result = await base.SaveChangesAsync( cancellationToken);
            await OnAfterSaveChanges(AuditEntries);
            return result;
        }


        private  List<AuditEntry> OnBeforeSaveChanges(int UserId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.Relational().TableName;
                auditEntry.UserId = UserId;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.OperationTypeId = 1;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.OperationTypeId = 3;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.OperationTypeId = 2;
                               
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }


        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

        #endregion AuditionSettig


        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {






            modelBuilder.HasSequence("assettype_id_seq");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HCMContext).Assembly);
            modelBuilder.HasSequence("documents_id_seq");
            modelBuilder.HasSequence("organization_id_seq");
            modelBuilder.HasSequence("processtracking_id_seq");
            modelBuilder.HasSequence("addresstype_id_seq");
            modelBuilder.HasSequence("assessmentlevel_id_seq");
            modelBuilder.HasSequence("assessmentrules_id_seq");
            modelBuilder.HasSequence("assettype_id_seq");
            modelBuilder.HasSequence("certification_id_seq");
            modelBuilder.HasSequence("characteristicresult_id_seq");
            modelBuilder.HasSequence("complaintype_id_seq");
            modelBuilder.HasSequence("component_id_seq");
            modelBuilder.HasSequence("course_id_seq");
            modelBuilder.HasSequence("currency_id_seq");
            modelBuilder.HasSequence("direction_id_seq");
            modelBuilder.HasSequence<int>("DocumentType_ID_seq");

            modelBuilder.HasSequence("educationlevel_id_seq");

            modelBuilder.HasSequence("employmentstatus_id_seq");

            modelBuilder.HasSequence("ethnicity_id_seq");

            modelBuilder.HasSequence("evaluationmark_id_seq");

            modelBuilder.HasSequence("evaluationtype_id_seq");

            modelBuilder.HasSequence("eventlock_id_seq");

            modelBuilder.HasSequence("eventtype_id_seq");

            modelBuilder.HasSequence("expertise_id_seq");

            modelBuilder.HasSequence("externalrelationship_id_seq");

            modelBuilder.HasSequence("filelocation_id_seq");

            modelBuilder.HasSequence("fundlevel_id_seq");

            modelBuilder.HasSequence("gender_id_seq");

            modelBuilder.HasSequence("habit_id_seq");

            modelBuilder.HasSequence("healthreporttopic_id_seq");

            modelBuilder.HasSequence("identificationtype_id_seq");

            modelBuilder.HasSequence("indicator_id_seq");

            modelBuilder.HasSequence("institutetype_id_seq");

            modelBuilder.HasSequence("jobstatus_id_seq");

            modelBuilder.HasSequence("language_id_seq");

            modelBuilder.HasSequence("leavetype_id_seq");

            modelBuilder.HasSequence("location_id_seq");

            modelBuilder.HasSequence("major_id_seq");

            modelBuilder.HasSequence("maritalstatus_id_seq");

            modelBuilder.HasSequence("nationality_id_seq");

            modelBuilder.HasSequence("objectlevel_id_seq");

            modelBuilder.HasSequence("objecttype_id_seq");

            modelBuilder.HasSequence("observation_id_seq");

            modelBuilder.HasSequence("offdutyreason_id_seq");

            modelBuilder.HasSequence("ordertype_id_seq");

            modelBuilder.HasSequence("organizationtype_id_seq");

            modelBuilder.HasSequence("orgunittype_id_seq");

            modelBuilder.HasSequence("politicalrelation_id_seq");

            modelBuilder.HasSequence("positiontype_id_seq");

            modelBuilder.HasSequence("programtype_id_seq");

            modelBuilder.HasSequence("projecttype_id_seq");

            modelBuilder.HasSequence("publicationtype_id_seq");

            modelBuilder.HasSequence("rank_id_seq");

            modelBuilder.HasSequence("referencetype_id_seq");

            modelBuilder.HasSequence("relationship_id_seq");

            modelBuilder.HasSequence("religion_id_seq");

            modelBuilder.HasSequence("result_id_seq");

            modelBuilder.HasSequence("rewardtype_id_seq");

            modelBuilder.HasSequence("salarytype_id_seq");

            modelBuilder.HasSequence("screens_id_seq");

            modelBuilder.HasSequence("skilltype_id_seq");

            modelBuilder.HasSequence("status_id_seq");

            modelBuilder.HasSequence("subject_id_seq");

            modelBuilder.HasSequence("trainingtype_id_seq");

            modelBuilder.HasSequence("violationtype_id_seq");

            modelBuilder.HasSequence("organogram_id_seq");

            modelBuilder.HasSequence("orgunit_id_seq");

            modelBuilder.HasSequence("orgunitchange_id_seq");

            modelBuilder.HasSequence("position_id_seq");

            modelBuilder.HasSequence("positionchange_id_seq");

            modelBuilder.HasSequence("positionrequirements_id_seq");

            modelBuilder.HasSequence("positionresponsibility_id_seq");

            modelBuilder.HasSequence("complain_id_seq");

            modelBuilder.HasSequence("event_id_seq");

            modelBuilder.HasSequence("governmenthelp_id_seq");

            modelBuilder.HasSequence("owner_id_seq");

            modelBuilder.HasSequence("promotion_id_seq");

            modelBuilder.HasSequence("punishment_id_seq");

            modelBuilder.HasSequence("retirement_id_seq");

            modelBuilder.HasSequence("selection_id_seq");

            modelBuilder.HasSequence("address_id_seq");

            modelBuilder.HasSequence("education_id_seq");

            modelBuilder.HasSequence("experience_id_seq");

            modelBuilder.HasSequence("healthreport_id_seq");

            modelBuilder.HasSequence("idcard_id_seq");

            modelBuilder.HasSequence("militaryservice_id_seq");

            modelBuilder.HasSequence("person_id_seq");

            modelBuilder.HasSequence("personasset_id_seq");

            modelBuilder.HasSequence("personcharacteristic_id_seq");

            modelBuilder.HasSequence<int>("PersonDocument_ID_seq");

            modelBuilder.HasSequence("personidentification_id_seq");

            modelBuilder.HasSequence("personlanguage_id_seq");

            modelBuilder.HasSequence("personskill_id_seq");

            modelBuilder.HasSequence("publication_id_seq");

            modelBuilder.HasSequence("reference_id_seq");

            modelBuilder.HasSequence("relative_id_seq");

            modelBuilder.HasSequence("travel_id_seq");


            modelBuilder.HasSequence("processtracking_id_seq");

        }
        #endregion
    }
}
