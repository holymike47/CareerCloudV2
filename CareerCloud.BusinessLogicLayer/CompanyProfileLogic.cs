using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {

        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            //Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, $"{poco.Id}, please note:Only .ca, .com, .biz are supported "));
                }
               
               else if (!Regex.IsMatch(poco.CompanyWebsite,@"(?i)\.(ca|com|biz)$"))
                {
                    exceptions.Add(new ValidationException(600, $"{poco.Id}, please note:Only .ca, .com, .biz are supported "));
                }
                
                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"PhoneNumber for {poco.Id} is required and should be valid"));
                }
                
                else if (!Regex.IsMatch(poco.ContactPhone, @"(?i)\d{3}-\d{3}-\d{4}"))
                {
                    exceptions.Add(new ValidationException(601, $"PhoneNumber for {poco.Id} is required and should be valid"));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
