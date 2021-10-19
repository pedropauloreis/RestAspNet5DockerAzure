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
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserBusiness _userBussiness;

        public UserController(ILogger<UserController> logger, IUserBusiness userBussiness)
        {
            _logger = logger;
            _userBussiness = userBussiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<UserVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_userBussiness.FindAll());
        }


        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _userBussiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);

        }

   
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] UserVO user)
        {
            if (user == null) return BadRequest();
            return Ok(_userBussiness.Create(user));
        }


        [HttpPut]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] UserVO user)
        {
            if (user == null) return BadRequest();
            if (!_userBussiness.Exists(user.Id)) return BadRequest("Not found");
            return Ok(_userBussiness.Update(user));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _userBussiness.Delete(id);
            return NoContent();

        }

        [HttpPatch("{id}/enable")]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Enable(long id)
        {

            return Ok(_userBussiness.Enable(id));

        }

        [HttpPatch("{id}/disable")]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Disable(long id)
        {

            return Ok(_userBussiness.Disable(id));

        }


    }
}
