using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hahn.ApplicatonProcess.December2020.Model
{
    public class Applicant
    {
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
            //RuleFor(x => x.ID).NotNull();
            //RuleFor(x => x.ID).
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
            RuleFor(x => x.FamilyName).NotNull().MinimumLength(5);
            RuleFor(x => x.Address).NotNull().MinimumLength(10);
            //RuleFor(x => x.CountryOfOrigin)
            RuleFor(x => x.EmailAdress).Matches(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)$");
            RuleFor(x => x.Age).ExclusiveBetween(20, 60);

        }
    }
}
