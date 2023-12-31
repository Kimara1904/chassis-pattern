﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Interface;

namespace UserService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        public AuthenticationController(IAuthenticationService authService)
        {
            _service = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO newUser)
        {
            await _service.Register(newUser);
            return Ok(string.Format("Successfully registered user with username: {0}", newUser.Username));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credentials)
        {
            var result = await _service.Login(credentials);
            return Ok(result);
        }
    }
}
