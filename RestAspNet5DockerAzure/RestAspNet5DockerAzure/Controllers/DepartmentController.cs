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
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private IDepartmentBusiness _departmentBusiness;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentBusiness departmentBusiness)
        {
            _logger = logger;
            _departmentBusiness = departmentBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<DepartmentVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_departmentBusiness.FindAll());
        }


        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(DepartmentVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _departmentBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);

        }

   
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(DepartmentVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] DepartmentVO department)
        {
            if (department == null) return BadRequest();
            return Ok(_departmentBusiness.Create(department));
        }


        [HttpPut]
        [ProducesResponseType((200), Type = typeof(DepartmentVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] DepartmentVO department)
        {
            if (department == null) return BadRequest();
            if (!_departmentBusiness.Exists(department.Id)) return BadRequest("Not found");
            return Ok(_departmentBusiness.Update(department));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _departmentBusiness.Delete(id);
            return NoContent();

        }

    }
}
