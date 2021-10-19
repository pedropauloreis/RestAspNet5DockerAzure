using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspNet5DockerAzure.Business;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestAspNet5DockerAzure.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    [Authorize("Bearer")]
    [Authorize(Roles = "admin,superuser")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBussiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBussiness = personBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBussiness.FindAll());
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(
            [FromQuery] string firstname,
            [FromQuery] string lastname,
            [FromQuery] string sortfields,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            Dictionary<string, string> filters = new Dictionary<string, string>();
            if(!string.IsNullOrWhiteSpace(firstname))
                filters.Add("first_name", firstname);

            if (!string.IsNullOrWhiteSpace(lastname))
                filters.Add("last_name", lastname);

            List<string> sortFieldsList = new List<string>();
            if (!string.IsNullOrWhiteSpace(sortfields))
                sortFieldsList.AddRange(sortfields.Split(","));

            return Ok(_personBussiness.FindWithPagedSearch(filters, sortFieldsList, sortDirection,pageSize,page));
        }


        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBussiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);

        }

        [HttpGet("findbyname")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string firstname, [FromQuery] string lastname)
        {
            var person = _personBussiness.FindByName(firstname,lastname);
            if (person == null || person.Count == 0) return NotFound();
            return Ok(person);

        }


        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBussiness.Create(person));
        }


        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            if (!_personBussiness.Exists(person.Id)) return BadRequest("Not found");
            return Ok(_personBussiness.Update(person));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personBussiness.Delete(id);
            return NoContent();

        }

    }
}
