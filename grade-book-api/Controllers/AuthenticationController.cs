using System;
using ApplicationCore.Interfaces;
using grade_book_api.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserJwtAuthService _authService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserJwtAuthService authService)
        {
            _logger = logger;
            _authService = authService; 
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test(){
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
                    request.LastName);
                return Ok(new {});
            }
            catch (ApplicationException exception)
            {
                return Conflict(exception.Message); 
            }
        }
        
        [HttpPost]
        public IActionResult TryLogin([FromBody] AuthenticateRequest request)
        {

            string token = _authService.TryGetToken(request.UsernameOrEmail, request.Password);
            if (token is null)
                return Unauthorized();
            return Ok(new {User = request.UsernameOrEmail, Token = token}); 


        }
    }
}