using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.Repository
{
    public class ApplicantsRepository
    {
        public DBContext db { get; }

        public ApplicantsRepository(DBContext dbContext)
        {
            this.db = dbContext;
        }

        public IQueryable<Applicant> GetApplicants()
        {
            return db.Applicants;
        }

        public bool AddApplicant(Applicant applicant)
        {
            db.Applicants.Add(applicant);
            db.SaveChanges();

            return true;
        }
    }
}
