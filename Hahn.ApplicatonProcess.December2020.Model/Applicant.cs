using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Model
{
    public class Applicant
    {
        public Applicant()
        {
           
        }
        /// <summary>
        /// Unique Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// Applicant Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Last Name, Surname
        /// </summary>
        public string FamilyName { get; set; }
        ///// <summary>
        ///// Address
        ///// </summary>
        public string Address { get; set; }
        ///// <summary>
        ///// Country of Origin
        ///// </summary>
        public string CountryOfOrigin { get; set; }
        ///// <summary>
        ///// Email
        ///// </summary>
        public string EmailAdress { get; set; }
        ///// <summary>
        ///// Applicant Age
        ///// </summary>
        public int Age { get; set; }
        ///// <summary>
        ///// If hired
        ///// </summary>
       public bool? Hired { get; set; } = false;
    }

    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
            RuleFor(x => x.FamilyName).NotNull().MinimumLength(5);
            RuleFor(x => x.Address).NotNull().MinimumLength(10);            
            RuleFor(x => x.EmailAdress).Matches(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)$");
            RuleFor(x => x.Age).ExclusiveBetween(20, 60);

            RuleFor(x => x.CountryOfOrigin).MustAsync(async (country, cancellation) => {
                bool exists = await ValidateCountry(country);
                return exists;
            }).WithMessage("Country must be valid.");
        }

        public async Task<bool> ValidateCountry(string country)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = string.Format("https://restcountries.eu/rest/v2/name/{0}?fullText=true", country);
                //Post http callas.
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                return true;
            }
        }
    }    
}
