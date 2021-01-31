using Hahn.ApplicatonProcess.December2020.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly ApplicantsRepository _applicantsRepository;
        public ApplicantsController(ApplicantsRepository applicantsRepository)
        {
            _applicantsRepository = applicantsRepository;
        }

        // GET: api/<ApplicantsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _applicantsRepository.GetApplicants();
            return new string[] { "value1", "value2" };
        }

        // GET api/<ApplicantsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApplicantsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApplicantsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApplicantsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
