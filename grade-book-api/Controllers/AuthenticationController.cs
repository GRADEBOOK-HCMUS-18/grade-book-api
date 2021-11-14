using System;
using ApplicationCore.Interfaces;
using grade_book_api.Requests;
using grade_book_api.Responses.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserJwtAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserServices _userService;

        public AuthenticationController(ILogger<AuthenticationController> logger
            , IUserJwtAuthService authService
            , IUserServices userService)
        {
            _userService = userService;
            _logger = logger;
            _authService = authService;
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok(new {messenge = "It is running"});
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult TryRegister([FromBody] UserRegisterRequest request)
        {
            try
            {
                var newUser = _authService.CreateNewUser(request.Username,
                    request.Password,
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.ProfilePictureUrl,
                    request.DefaultProfilePictureHex);
                var tokenToSend = _authService.TryGetToken(request.Username, request.Password);
                var response = new LoginResponse
                {
                    Username = newUser.Username,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    ProfilePictureUrl = newUser.ProfilePictureUrl,
                    DefaultProfilePictureHex = newUser.DefaultProfilePictureHex,
                    Token = tokenToSend
                };
                return Ok(response);
            }
            catch (ApplicationException exception)
            {
                return Conflict(exception.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult TryLogin([FromBody] AuthenticateRequest request)
        {
            var foundUser = _userService.GetUserByNameOrEmail(request.UsernameOrEmail);

            if (foundUser is null) return Unauthorized("No user with that username or email");
            var token = _authService.TryGetToken(request.UsernameOrEmail, request.Password);
            if (token is null)
                return Unauthorized("Wrong credential");

            var response = new LoginResponse
            {
                Username = foundUser.Username,
                Email = foundUser.LastName,
                FirstName = foundUser.FirstName,
                LastName = foundUser.LastName,
                ProfilePictureUrl = foundUser.ProfilePictureUrl,
                DefaultProfilePictureHex = foundUser.DefaultProfilePictureHex,
                Token = token
            };
            return Ok(response);
        }
    }
}