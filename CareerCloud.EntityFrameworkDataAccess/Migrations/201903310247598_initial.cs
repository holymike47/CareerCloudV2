namespace CareerCloud.EntityFrameworkDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicant_Educations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Applicant = c.Guid(nullable: false),
                        Major = c.String(nullable: false, maxLength: 30),
                        Certificate_Diploma = c.String(maxLength: 20),
                        Start_Date = c.DateTime(),
                        Completion_Date = c.DateTime(),
                        Completion_Percent = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant_Profiles", t => t.Applicant, cascadeDelete: true)
                .Index(t => t.Applicant);
            
            CreateTable(
                "dbo.Applicant_Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.Guid(nullable: false),
                        Current_Salary = c.Decimal(precision: 18, scale: 2),
                        Current_Rate = c.Decimal(precision: 18, scale: 2),
                        Currency = c.String(maxLength: 3),
                        Country_Code = c.String(nullable: false, maxLength: 10),
                        State_Province_Code = c.String(maxLength: 2),
                        Street_Address = c.String(maxLength: 100),
                        City_Town = c.String(maxLength: 100),
                        Zip_Postal_Code = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Country_Codes", t => t.Country_Code, cascadeDelete: true)
                .Index(t => t.Country_Code);
            
            CreateTable(
                "dbo.Applicant_Job_Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Applicant = c.Guid(nullable: false),
                        Job = c.Guid(nullable: false),
                        Application_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company_Jobs", t => t.Job, cascadeDelete: true)
                .ForeignKey("dbo.Applicant_Profiles", t => t.Applicant, cascadeDelete: true)
                .Index(t => t.Applicant);
            
            CreateTable(
                "dbo.Company_Jobs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Company = c.Guid(nullable: false),
                        Profile_Created = c.DateTime(nullable: false),
                        Is_Inactive = c.Boolean(nullable: false),
                        Is_Company_Hidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company_Profiles", t => t.Company, cascadeDelete: true)
                .Index(t => t.Company);
            
            CreateTable(
                "dbo.Company_Jobs_Descriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Job = c.Guid(nullable: false),
                        Job_Name = c.String(maxLength: 100),
                        Job_Descriptions = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company_Jobs", t => t.Job, cascadeDelete: true)
                .Index(t => t.Job);
            
            CreateTable(
                "dbo.Company_Job_Educations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Job = c.Guid(nullable: false),
                        Major = c.String(nullable: false, maxLength: 100),
                        Importance = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company_Jobs", t => t.Job, cascadeDelete: true)
                .Index(t => t.Job);
            
            CreateTable(
                "dbo.Company_Job_Skills",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Job = c.Guid(nullable: false),
                        Skill = c.String(nullable: false, maxLength: 50),
                        Skill_Level = c.String(nullable: false, maxLength: 10),
                        Importance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company_Jobs", t => t.Job, cascadeDelete: true)
                .Index(t => t.Job);
            
            CreateTable(
                "dbo.Company_Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Registration_Date = c.DateTime(nullable: false),
                        Company_Website = c.String(maxLength: 100),
                        Contact_Phone = c.String(nullable: false, maxLength: 20),
                        Contact_Name = c.String(maxLength: 50),
                        Company_Logo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Company_Descriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Company = c.Guid(nullable: false),
                        LanguageId = c.String(nullable: false, maxLength: 10),
                        Company_Name = c.String(nullable: false, maxLength: 4),
                        Company_Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Language_Codes", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Company_Profiles", t => t.Company, cascadeDelete: true)
                .Index(t => t.Company)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.System_Language_Codes",
                c => new
                    {
                        LanguageID = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 50),
                        Native_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.Company_Locations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Company = c.Guid(nullable: false),
                        Country_Code = c.String(nullable: false, maxLength: 3),
                        State_Province_Code = c.String(maxLength: 2),
                        Street_Address = c.String(maxLength: 100),
                        City_Town = c.String(maxLength: 100),
                        Zip_Postal_Code = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company_Profiles", t => t.Company, cascadeDelete: true)
                .Index(t => t.Company);
            
            CreateTable(
                "dbo.Applicant_Resumes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Applicant = c.Guid(nullable: false),
                        Resume = c.String(nullable: false),
                        Last_Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant_Profiles", t => t.Applicant, cascadeDelete: true)
                .Index(t => t.Applicant);
            
            CreateTable(
                "dbo.Applicant_Skills",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Applicant = c.Guid(nullable: false),
                        Skill = c.String(nullable: false, maxLength: 30),
                        Skill_Level = c.String(nullable: false, maxLength: 10),
                        Start_Month = c.Byte(nullable: false),
                        Start_Year = c.Int(nullable: false),
                        End_Month = c.Byte(nullable: false),
                        End_Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicant_Profiles", t => t.Applicant, cascadeDelete: true)
                .Index(t => t.Applicant);
            
            CreateTable(
                "dbo.Applicant_Work_History",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Applicant = c.Guid(nullable: false),
                        Company_Name = c.String(nullable: false, maxLength: 150),
                        Country_Code = c.String(nullable: false, maxLength: 10),
                        Location = c.String(nullable: false, maxLength: 50),
                        Job_Title = c.String(nullable: false, maxLength: 50),
                        Job_Description = c.String(nullable: false, maxLength: 500),
                        Start_Month = c.Short(nullable: false),
                        Start_Year = c.Int(nullable: false),
                        End_Month = c.Short(nullable: false),
                        End_Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Country_Codes", t => t.Country_Code, cascadeDelete: true)
                .ForeignKey("dbo.Applicant_Profiles", t => t.Applicant, cascadeDelete: false)
                .Index(t => t.Applicant)
                .Index(t => t.Country_Code);
            
            CreateTable(
                "dbo.System_Country_Codes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 10),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant_Work_History", "Applicant", "dbo.Applicant_Profiles");
            DropForeignKey("dbo.Applicant_Work_History", "Country_Code", "dbo.System_Country_Codes");
            DropForeignKey("dbo.Applicant_Profiles", "Country_Code", "dbo.System_Country_Codes");
            DropForeignKey("dbo.Applicant_Skills", "Applicant", "dbo.Applicant_Profiles");
            DropForeignKey("dbo.Applicant_Resumes", "Applicant", "dbo.Applicant_Profiles");
            DropForeignKey("dbo.Applicant_Job_Applications", "Applicant", "dbo.Applicant_Profiles");
            DropForeignKey("dbo.Company_Locations", "Company", "dbo.Company_Profiles");
            DropForeignKey("dbo.Company_Jobs", "Company", "dbo.Company_Profiles");
            DropForeignKey("dbo.Company_Descriptions", "Company", "dbo.Company_Profiles");
            DropForeignKey("dbo.Company_Descriptions", "LanguageId", "dbo.System_Language_Codes");
            DropForeignKey("dbo.Company_Job_Skills", "Job", "dbo.Company_Jobs");
            DropForeignKey("dbo.Company_Job_Educations", "Job", "dbo.Company_Jobs");
            DropForeignKey("dbo.Company_Jobs_Descriptions", "Job", "dbo.Company_Jobs");
            DropForeignKey("dbo.Applicant_Job_Applications", "Applicant", "dbo.Company_Jobs");
            DropForeignKey("dbo.Applicant_Educations", "Applicant", "dbo.Applicant_Profiles");
            DropIndex("dbo.Applicant_Work_History", new[] { "Country_Code" });
            DropIndex("dbo.Applicant_Work_History", new[] { "Applicant" });
            DropIndex("dbo.Applicant_Skills", new[] { "Applicant" });
            DropIndex("dbo.Applicant_Resumes", new[] { "Applicant" });
            DropIndex("dbo.Company_Locations", new[] { "Company" });
            DropIndex("dbo.Company_Descriptions", new[] { "LanguageId" });
            DropIndex("dbo.Company_Descriptions", new[] { "Company" });
            DropIndex("dbo.Company_Job_Skills", new[] { "Job" });
            DropIndex("dbo.Company_Job_Educations", new[] { "Job" });
            DropIndex("dbo.Company_Jobs_Descriptions", new[] { "Job" });
            DropIndex("dbo.Company_Jobs", new[] { "Company" });
            DropIndex("dbo.Applicant_Job_Applications", new[] { "Applicant" });
            DropIndex("dbo.Applicant_Profiles", new[] { "Country_Code" });
            DropIndex("dbo.Applicant_Educations", new[] { "Applicant" });
            DropTable("dbo.System_Country_Codes");
            DropTable("dbo.Applicant_Work_History");
            DropTable("dbo.Applicant_Skills");
            DropTable("dbo.Applicant_Resumes");
            DropTable("dbo.Company_Locations");
            DropTable("dbo.System_Language_Codes");
            DropTable("dbo.Company_Descriptions");
            DropTable("dbo.Company_Profiles");
            DropTable("dbo.Company_Job_Skills");
            DropTable("dbo.Company_Job_Educations");
            DropTable("dbo.Company_Jobs_Descriptions");
            DropTable("dbo.Company_Jobs");
            DropTable("dbo.Applicant_Job_Applications");
            DropTable("dbo.Applicant_Profiles");
            DropTable("dbo.Applicant_Educations");
        }
    }
}
