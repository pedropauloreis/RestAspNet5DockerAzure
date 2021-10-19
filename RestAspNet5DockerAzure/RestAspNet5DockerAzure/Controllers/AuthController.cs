﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAspNet5DockerAzure.Business;
using RestAspNet5DockerAzure.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5DockerAzure.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client request");
            
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(tokenVO);
            if (token == null) return BadRequest("Invalid client request");

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Invalid client request");

            return NoContent();
        }
    }
}
