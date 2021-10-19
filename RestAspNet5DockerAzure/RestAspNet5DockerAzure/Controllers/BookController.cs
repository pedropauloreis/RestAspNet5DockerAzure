using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspNet5DockerAzure.Data.VO;
using RestAspNet5DockerAzure.Business;
using RestAspNet5DockerAzure.Hypermedia.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RestAspNet5DockerAzure.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    [Authorize("Bearer")]
    [Authorize(Roles = "admin,superuser,user")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBussiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBussiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBussiness.FindAll());
        }


        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBussiness.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);

        }


        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize(Roles = "admin,superuser")]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBussiness.Create(book));
        }


        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize(Roles = "admin,superuser")]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            if (!_bookBussiness.Exists(book.Id)) return BadRequest();
            return Ok(_bookBussiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "admin,superuser")]
        public IActionResult Delete(long id)
        {
            _bookBussiness.Delete(id);
            return NoContent();

        }

    }
}
