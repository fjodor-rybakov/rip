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
            var token = _authService.Login(loginUserDto);
            return new OkObjectResult(new {Token = token});
        }

        [HttpPost("registration")]
        public ActionResult Registration([FromBody] CreateUserDto createUserDto)
        {
            var registrationResult = _authService.Registration(createUserDto);
            return new OkObjectResult(new {Message = registrationResult});
        }
    }
}