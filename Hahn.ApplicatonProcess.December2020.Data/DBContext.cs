using Hahn.ApplicatonProcess.December2020.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
           // var options = new DbContextOptionsBuilder<DBContext>()
           //         .UseInMemoryDatabase(databaseName: "Test")
           //         .Options;
        }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicantEntityConfiguration());
        }
    }
}
