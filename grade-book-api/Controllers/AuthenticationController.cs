using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using grade_book_api.Requests;
using grade_book_api.Responses.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grade_book_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserJwtAuthService _authService;
        private readonly IUserServices _userService;
        private readonly IEmailSender _emailSender;

        public AuthenticationController(
             IUserJwtAuthService authService
            , IUserServices userService, IEmailSender emailSender)
        {
            _userService = userService;
            _emailSender = emailSender;
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
            if (foundUser.IsLocked)
                return Unauthorized("Your account is locked");
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
            if (existedUser is not null)
            {
                if (existedUser.IsLocked)
                    return Unauthorized("Your account is locked"); 
            }
            if (existedUser is null)
                existedUser = _authService.CreateNewUser("", request.Email, request.FirstName, request.LastName,
                    request.ProfilePictureUrl, request.DefaultProfilePictureHex);
            var tokenWithoutPassword = _authService.TryGetTokenWithoutPassword(request.Email);
            return Ok(new LoginResponse(existedUser, tokenWithoutPassword));
        }
        
        [HttpPost("confirmation")]

        public async Task<IActionResult> TryAddNewAccountConfirmationRequest()
        {
            int currentUserId = GetCurrentUserIdFromToken();
            var existedUser = _userService.GetUserById(currentUserId);
            if (existedUser is null)
                return BadRequest("User does not exist");
            if (existedUser.IsLocked)
                return BadRequest($"Account {existedUser.Email} is locked");

            var newRequest = _authService.CreateNewConfirmationRequest(existedUser.Email);

            try
            {
                await _emailSender.SendEmail(existedUser.Email,
                    "GradeBook: Confirm your account", 
                    $"Confirm your account with the following code: {newRequest.ConfirmationCode}");

            }
            catch(Exception ex)
            {
                return BadRequest($"Error while sending the email: {ex.Message}");
            }

            return Ok($"Sent email to address {existedUser.Email}");
        }
        
        [HttpPost("confirmation/code")]
        public IActionResult TryConfirmAccount(TryConfirmationRequest request)
        {
            int currentUserId = GetCurrentUserIdFromToken();
            try
            {
                _ = _authService.UpdateEmailConfirmationState(currentUserId, request.ConfirmationCode);
                return Ok("Confirmed email");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> TryAddNewForgotPasswordRequest(AddNewForgotPasswordRequest request)
        {
            
            var existedUser = _userService.GetUserByEmail(request.Email);
            if (existedUser is null)
                return BadRequest("User does not exist");
            if (existedUser.IsLocked)
                return BadRequest($"Account {existedUser.Email} is locked");

            var newRequest = _authService.CreateNewForgotPasswordRequest(request.Email);
            string emailSubject = "GradeBook: Change your password";
            string emailContent =
                $"Change your password using the following confirmation code {newRequest.ConfirmationCode}";
            try
            {
                await _emailSender.SendEmail(request.Email, emailSubject, emailContent);
            }
            catch (Exception ex)
            {
                return BadRequest($"Email error: {ex.Message}");
            }

            return Ok($"Send email to the address {request.Email}");
        }


        [AllowAnonymous]
        [HttpPost("forgotPassword/code")]
        public IActionResult TryConfirmForgotPasswordRequest(TryConfirmCodeWhenForgotPassword request)
        {
            try
            {
                string newToken = _authService.UpdatePasswordWithCode(request.Email, request.ConfirmationCode);
                return Ok(new {token = newToken});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        private int GetCurrentUserIdFromToken()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return userId;
        }
    }
}