using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.Repository
{
    //public class ApplicantsRepository
    //{
    //    public DBContext db { get; }

    //    public ApplicantsRepository(DBContext dbContext)
    //    {
    //        this.db = dbContext;
    //    }

    //    public IQueryable<Applicant> GetApplicants()
    //    {
    //        return db.Applicants;
    //    }

    //    public bool AddApplicant(Applicant applicant)
    //    {
    //        db.Applicants.Add(applicant);
    //        db.SaveChanges();

    //        return true;
    //    }
    //}

    public class ApplicantsRepository : RepositoryBase<Applicant>
    {
        public ApplicantsRepository(DBContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public new int Create(Applicant applicant)
        {
            base.Create(applicant);
            Save();
            return applicant.ID;
        }

        public Applicant Get(int id)
        {
            return FindByCondition(a => a.ID == id).FirstOrDefault();
        }

        public bool Update(int id, Applicant applicant)
        {
            Applicant current = FindByCondition(a => a.ID == id).FirstOrDefault();

            if (current == null)
            {
                // return, applicant with this id does not exist
                return false;
            }

            applicant.ID = id;
            Update(applicant, current);
            Save();

            return true;
        }

        public bool Delete(int id)
        {
            Applicant current = base.FindByCondition(a => a.ID == id).FirstOrDefault();

            if (current == null)
            {
                return false;
            }

            Delete(current);
            Save();

            return true;
        }
    }
}
