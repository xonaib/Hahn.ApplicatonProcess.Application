using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        //HttpClient _client;

        public ApplicantsRepository(DBContext repositoryContext)
        : base(repositoryContext)
        {
            //_client = client;
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

            bool result = ValidateCountry(applicant.CountryOfOrigin).Result;
            if(!result)
            {
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

        public async Task<bool> ValidateCountry(string country)
        {
            using(HttpClient client = new HttpClient())
            {
                string apiUrl = string.Format("https://restcountries.eu/rest/v2/name/{0}?fullText=true", country);
                //Post http callas.
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
