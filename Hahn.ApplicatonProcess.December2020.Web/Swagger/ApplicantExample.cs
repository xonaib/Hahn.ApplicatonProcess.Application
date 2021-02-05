using Hahn.ApplicatonProcess.December2020.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Swagger
{
    public class ApplicantPostExample: IExamplesProvider<Applicant>
    {
        // IExamplesProvider<Applicant>.GetExamples
        public Applicant GetExamples()
        {
            return new Applicant
            {                
                Name= "John",
                FamilyName = "Doeish",
                Address = "Random address in some country",
                EmailAdress = "abc@xyz.com",
                Age = 23,
                
            };
        }
    }
}
