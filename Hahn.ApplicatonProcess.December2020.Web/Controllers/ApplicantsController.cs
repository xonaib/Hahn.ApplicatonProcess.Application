using Hahn.ApplicatonProcess.December2020.Domain.Repository;
using Hahn.ApplicatonProcess.December2020.Model;
using Hahn.ApplicatonProcess.December2020.Web.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly ApplicantsRepository _applicantsRepository;
        //private readonly ApplicantsRespository2 _applicantsRespository2;
        public ApplicantsController(ApplicantsRepository applicantsRepository)
        {
            _applicantsRepository = applicantsRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET applicants
        /// 
        /// Sample Response
        /// 
        ///     {
        ///         "id": 1,
        ///         "name": "aa",
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns all items</response>
        /// <response code="400">If there was an error processing the request</response>
        // GET: api/<ApplicantsController>
        [HttpGet]
        [Produces("application/json")]
        //[SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Applicant), Description = "Delivery options for the country found and returned successfully")]
        //[SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ApplicantExample))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "An invalid or missing input parameter will result in a bad request")]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "An unexpected error occurred, should not return sensitive information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //(int)HttpStatusCode.BadRequest)
        public IEnumerable<Applicant> Get()
        {
            //IEnumerable<Applicant> applicants =  _applicantsRepository.GetApplicants().ToList();
            //return applicants;
            IEnumerable<Applicant> applicants = _applicantsRepository.FindAll();
            return applicants;
        }

        // GET api/<ApplicantsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApplicantsController>
        [HttpPost]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantPostExample))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromBody] Applicant value)
        {
            if(value != null && ModelState.IsValid)
            {
                int id = _applicantsRepository.Create(value);

                return Created("abc", id);
                //_applicantsRepository.AddApplicant(value);
            }

            return BadRequest();
            //return StatusCode(StatusCodes.Status400BadRequest);
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
