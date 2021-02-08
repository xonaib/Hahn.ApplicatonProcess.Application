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
using Serilog;
using Microsoft.Extensions.Logging;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApplicantsController : ControllerBase
    {
        private readonly ApplicantsRepository _applicantsRepository;
        private readonly ILogger<ApplicantsController> _logger;

        //private readonly ApplicantsRespository2 _applicantsRespository2;
        public ApplicantsController(ApplicantsRepository applicantsRepository, ILogger<ApplicantsController> logger)
        {
            _applicantsRepository = applicantsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get all applicants
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
        //[Produces("application/json")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Applicant), Description = "Delivery options for the country found and returned successfully")]
        //[SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ApplicantExample))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "An invalid or missing input parameter will result in a bad request")]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "An unexpected error occurred, should not return sensitive information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //(int)HttpStatusCode.BadRequest)
        public IEnumerable<Applicant> Get()
        {
                    
            IEnumerable<Applicant> applicants = _applicantsRepository.FindAll();
            return applicants;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Numeric id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Log.Information($"Get Applicant, id={id}");
            Applicant applicant = _applicantsRepository.Get(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return Ok(applicant);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// </remarks>
        /// <param name="value">
        /// 
        ///     {
        ///        "name": "fffff",
        ///         "familyName": "xxxxx",
        ///         "address": "adlkal kdlkaldk",
        ///         "countryOfOrigin": "pakistan",
        ///         "emailAdress": "zz@jj",
        ///         "age": 33,
        ///         "hired": true
        ///     }
        /// </param>
        /// <returns></returns>
        // POST <ApplicantsController>
        [HttpPost]
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantPostExample))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Post([FromBody] Applicant value)
        {
            string guid = Guid.NewGuid().ToString();
            Log.Information($"Post Applicant, guid {guid}, with value {value}");

            if (value != null && ModelState.IsValid)
            {
                int id = _applicantsRepository.Create(value);

                Log.Information($"Post Applicant, guid {guid}, status=201");
                string fetchUrl = $"/{id}";
                return Created(fetchUrl, fetchUrl);
            }

            Log.Information($"Post Applicant, guid {guid}, status=400");
            return BadRequest();
        }

        // PUT api/<ApplicantsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] Applicant value)
        {
            string guid = Guid.NewGuid().ToString();
            Log.Information($"Put Applicant, guid {guid}, with value {value}");

            if (value != null && ModelState.IsValid)
            {
                bool result = _applicantsRepository.Update(id, value);
                if (result)
                {
                    Log.Information($"Put Applicant, guid={guid}, status=200");
                    return Ok();
                }
            }

            Log.Information($"Post Applicant, guid={guid}, status=400");
            return BadRequest();
        }

        // DELETE api/<ApplicantsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            bool result = _applicantsRepository.Delete(id);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
