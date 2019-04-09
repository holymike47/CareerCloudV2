namespace CareerCloud.EntityFrameworkDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_countrycodes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applicant_Profiles", "Country_Code", "dbo.System_Country_Codes");
            DropForeignKey("dbo.Applicant_Work_History", "Country_Code", "dbo.System_Country_Codes");
            DropIndex("dbo.Applicant_Profiles", new[] { "Country_Code" });
            DropIndex("dbo.Applicant_Work_History", new[] { "Country_Code" });
            DropPrimaryKey("dbo.System_Country_Codes");
            AlterColumn("dbo.Applicant_Profiles", "Country_Code", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Applicant_Work_History", "Country_Code", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.System_Country_Codes", "Code", c => c.String(nullable: false, maxLength: 3));
            AddPrimaryKey("dbo.System_Country_Codes", "Code");
            CreateIndex("dbo.Applicant_Profiles", "Country_Code");
            CreateIndex("dbo.Applicant_Work_History", "Country_Code");
            AddForeignKey("dbo.Applicant_Profiles", "Country_Code", "dbo.System_Country_Codes", "Code", cascadeDelete: true);
            AddForeignKey("dbo.Applicant_Work_History", "Country_Code", "dbo.System_Country_Codes", "Code", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicant_Work_History", "Country_Code", "dbo.System_Country_Codes");
            DropForeignKey("dbo.Applicant_Profiles", "Country_Code", "dbo.System_Country_Codes");
            DropIndex("dbo.Applicant_Work_History", new[] { "Country_Code" });
            DropIndex("dbo.Applicant_Profiles", new[] { "Country_Code" });
            DropPrimaryKey("dbo.System_Country_Codes");
            AlterColumn("dbo.System_Country_Codes", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Applicant_Work_History", "Country_Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Applicant_Profiles", "Country_Code", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.System_Country_Codes", "Code");
            CreateIndex("dbo.Applicant_Work_History", "Country_Code");
            CreateIndex("dbo.Applicant_Profiles", "Country_Code");
            AddForeignKey("dbo.Applicant_Work_History", "Country_Code", "dbo.System_Country_Codes", "Code", cascadeDelete: true);
            AddForeignKey("dbo.Applicant_Profiles", "Country_Code", "dbo.System_Country_Codes", "Code", cascadeDelete: true);
        }
    }
}
