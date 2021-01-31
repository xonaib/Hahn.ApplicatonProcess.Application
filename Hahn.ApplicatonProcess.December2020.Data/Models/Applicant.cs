using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Data.Models
{
    public class Applicant
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Applicant Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Last Name, Surname
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Country of Origin
        /// </summary>
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string EmailAdress { get; set; }
        /// <summary>
        /// Applicant Age
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// If hired
        /// </summary>
        public bool Hired { get; set; } = false;
    }

    public class ApplicantEntityConfiguration: IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.Property
        } 
    }
}
