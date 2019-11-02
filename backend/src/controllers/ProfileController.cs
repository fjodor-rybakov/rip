using System.Linq;
using backend.models.assets;
using backend.models.dto.user;
using backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly AuthService _authService;

        public ProfileController(ProfileService profileService, AuthService authService)
        {
            _profileService = profileService;
            _authService = authService;
        }
        
        [HttpGet]
        [Authorize(Roles = AcceptRole.User + ", " + AcceptRole.Administrator)]
        public ActionResult<UserDto> GetProfile()
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            var tokenData = _authService.GetTokenInfo(token.ToString().Split(" ").Last());
            return new OkObjectResult(_profileService.GetProfile(tokenData.UserId));
        }
    }
}