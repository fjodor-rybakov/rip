using System;
using backend.helper;
using backend.models.dto.user;
using backend.services;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ApiErrors _apiErrors;

        public AuthController(AuthService authService, ApiErrors apiErrors)
        {
            _authService = authService;
            _apiErrors = apiErrors;
        }
        
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var token = _authService.Login(loginUserDto);
                return token == null ? throw _apiErrors.UserNotFount : new OkObjectResult(new {Token = token});
            }
            catch (Error error)
            {
                if (error.GetType().GetProperty("HttpStatus") != null)
                    return StatusCode(error.HttpStatus, error.Message);
                Console.WriteLine(error);
                return StatusCode(_apiErrors.ServerError.HttpStatus, _apiErrors.ServerError.Message);
            }
        }

        /*[HttpPost("registration")]
        public ActionResult Registration([FromBody] CreateUserDto createUserDto)
        {
            
        }*/
    }
}