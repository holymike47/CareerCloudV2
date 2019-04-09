namespace CareerCloud.EntityFrameworkDataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CareerCloud.EntityFrameworkDataAccess;
    using CareerCloud.Pocos;

    internal sealed class Configuration : DbMigrationsConfiguration<CareerCloudContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CareerCloudContext context)
        {
            List<SystemCountryCodePoco> SystemCountryCodes = new List<SystemCountryCodePoco>
            {
                new SystemCountryCodePoco{Code="CAN",Name="Canada"},
                new SystemCountryCodePoco{Code="ARG",Name="Argentina"},
                new SystemCountryCodePoco{Code="AUT",Name="Austria"},
                new SystemCountryCodePoco{Code="BEL",Name="Belgium"},
                new SystemCountryCodePoco{Code="BRA",Name="Brazil"},
                new SystemCountryCodePoco{Code="FRA",Name="France"},
                new SystemCountryCodePoco{Code="DEU",Name="Germany"},
                new SystemCountryCodePoco{Code="MEX",Name="Mexico"},
                new SystemCountryCodePoco{Code="USA",Name="USA"},
                new SystemCountryCodePoco{Code="GBR",Name="United Kingdom"}
            };
            List<SystemLanguageCodePoco> systemLanguageCodes = new List<SystemLanguageCodePoco>
            {
                new SystemLanguageCodePoco{LanguageID="ENG",Name="English",NativeName="English"},
                new SystemLanguageCodePoco{LanguageID="FRA",Name="French",NativeName="Français"},
                new SystemLanguageCodePoco{LanguageID="SPA",Name="Spanish",NativeName="Español"},
                new SystemLanguageCodePoco{LanguageID="DEU",Name="German",NativeName="Deutsch"},
                new SystemLanguageCodePoco{LanguageID="IND",Name="Indonesian",NativeName="Bahasa "},
                new SystemLanguageCodePoco{LanguageID="LAT",Name="Latin",NativeName="Lingua Latina"},
                new SystemLanguageCodePoco{LanguageID="MAY",Name="Malay",NativeName="Bahasa Melayu"},
                new SystemLanguageCodePoco{LanguageID="MLT",Name="Maltese",NativeName="Malti"},
                new SystemLanguageCodePoco{LanguageID="RON",Name="Romanian",NativeName="Română"},
                new SystemLanguageCodePoco{LanguageID="SLV",Name="Slovenian",NativeName="Slovenski Jezik"},
                new SystemLanguageCodePoco{LanguageID="SWE",Name="Swedish",NativeName="Svenska"},
            };
            foreach (SystemCountryCodePoco poco in SystemCountryCodes)
            {
                context.SystemCountryCode.AddOrUpdate(poco);
            }
            foreach(SystemLanguageCodePoco poco in systemLanguageCodes)
            {
                context.SystemLanguageCode.AddOrUpdate(poco);
            }
            context.SaveChanges();

        }
    }
}
