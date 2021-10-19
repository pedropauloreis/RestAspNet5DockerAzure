using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAspNet5DockerAzure.Business;
using RestAspNet5DockerAzure.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize(Roles = "admin,superuser,user")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;
        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet("uploadedfiles/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
           
            try
            {
                byte[] buffer = _fileBusiness.GetFile(fileName);
                if (buffer != null)
                {
                    HttpContext.Response.ContentType =
                        $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                    HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                    await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                }
                return new ContentResult();
            }
            catch (Exception)
            {
                return BadRequest("File not Found.");
            }
            

        }

        [Authorize(Roles = "admin,superuser")]
        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            try
            {
                FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);
                return new OkObjectResult(detail);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }



    }
}
