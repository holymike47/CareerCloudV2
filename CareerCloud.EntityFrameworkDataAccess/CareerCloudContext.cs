using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext:DbContext
    {
        public CareerCloudContext(bool createProxy = true) :base("name=CareerCloudContextv2")
        {
            Configuration.ProxyCreationEnabled = createProxy;
        }
        public CareerCloudContext():base("name=CareerCloudContextv2")
        {

        }
        public virtual DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public virtual DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }
        public virtual DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }
        public virtual DbSet<ApplicantResumePoco> ApplicantResume { get; set; }
        public virtual DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }
        public virtual DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public virtual DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }
        public virtual DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
        public virtual DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public virtual DbSet<CompanyJobPoco> CompanyJob { get; set; }
        public virtual DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
        public virtual DbSet<CompanyLocationPoco> CompanyLocation { get; set; }
        public virtual DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
        public virtual DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }
        public virtual DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////  ApplicantProfilePoco  ////
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(m => m.ApplicantEducations)
                .WithRequired(r => r.ApplicantProfile)
                .HasForeignKey(k => k.Applicant);


            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(m => m.ApplicantJobApplications)
                .WithRequired(r => r.ApplicantProfile)
                .HasForeignKey(k => k.Applicant);


            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(m => m.ApplicantResumes)
                .WithRequired(r => r.ApplicantProfile)
                .HasForeignKey(k => k.Applicant);


            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(m => m.ApplicantSkills)
                .WithRequired(r => r.ApplicantProfile)
                .HasForeignKey(k => k.Applicant);


            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(m => m.ApplicantWorkHistories)
                .WithRequired(r => r.ApplicantProfile)
                .HasForeignKey(k => k.Applicant);

            


            //// CompanyJobPoco ////
            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(m => m.ApplicantJobApplications)
                .WithRequired(r => r.CompanyJob)
                .HasForeignKey(k => k.Applicant);


            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(m => m.CompanyJobSkills)
                .WithRequired(r => r.CompanyJob)
                .HasForeignKey(k => k.Job);


            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(m => m.CompanyJobDescriptions)
                .WithRequired(r => r.CompanyJob)
                .HasForeignKey(k => k.Job);


            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(m => m.CompanyJobEducations)
                .WithRequired(r => r.CompanyJob)
                .HasForeignKey(k => k.Job);


            ////  CompanyProfilePoco  ////
            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(m => m.CompanyLocations)
                .WithRequired(r => r.CompanyProfile)
                .HasForeignKey(k => k.Company);


            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(m => m.CompanyDescriptions)
                .WithRequired(r => r.CompanyProfile)
                .HasForeignKey(k => k.Company);


            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(m => m.CompanyJobs)
                .WithRequired(r => r.CompanyProfile)
                .HasForeignKey(k => k.Company);


            //// SystemCountryCodePoco ////
            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(m => m.ApplicantProfiles)
                .WithRequired(r => r.SystemCountryCode)
                .HasForeignKey(k => k.Country);


            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(m => m.ApplicantWorkHistories)
                .WithRequired(r => r.SystemCountryCode)
                .HasForeignKey(k => k.CountryCode);


            //// SystemLanguageCodePoco  ////
            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(m => m.CompanyDescriptions)
                .WithRequired(r => r.SystemLanguageCode)
                .HasForeignKey(k => k.LanguageId);
                

        }
    }
}
