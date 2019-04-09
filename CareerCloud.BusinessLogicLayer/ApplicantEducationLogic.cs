using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic:BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository):base(repository)
        {
         
        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            //Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if ((string.IsNullOrEmpty(poco.Major)) || (poco.Major.Length < 3))
                {
                    exceptions.Add(new ValidationException(107, $"Major for {poco.Id} must be 3 or more Characters"));
                }
                if (poco.StartDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, $"Start Date for {poco.Id} cannot be greater than Today's date"));
                }
                if (poco.CompletionDate < poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109, $"{poco.Id}: Completion date cannot be earlier that Start date"));
                }
            }
            if(exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
