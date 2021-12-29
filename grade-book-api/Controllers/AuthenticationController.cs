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
                var newUser = _authService.CreateNewUser(
                    request.Password,
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.ProfilePictureUrl,
                    request.DefaultProfilePictureHex);
                var tokenToSend = _authService.TryGetToken(request.Email, request.Password);
                var response = new LoginResponse(newUser, tokenToSend);
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
            var foundUser = _userService.GetUserByEmail(request.Email);

            if (foundUser is null) return Unauthorized("No user with that username or email");
            var token = _authService.TryGetToken(request.Email, request.Password);
            if (token is null)
                return Unauthorized("Wrong credential");

            var response = new LoginResponse(foundUser, token);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public IActionResult TryAuthenticateGoogle([FromBody] GoogleAuthenticateRequest request)
        {
            // check if user existed 
            var existedUser = _userService.GetUserByEmail(request.Email);
            if (existedUser is null)
                existedUser = _authService.CreateNewUser("", request.Email, request.FirstName, request.LastName,
                    request.ProfilePictureUrl, request.DefaultProfilePictureHex);
            var tokenWithoutPassword = _authService.TryGetTokenWithoutPassword(request.Email);
            return Ok(new LoginResponse(existedUser, tokenWithoutPassword));
        }
    }
}