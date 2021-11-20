using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApplicationCore.Interfaces;
using grade_book_api.Requests;
using grade_book_api.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;

        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetUserInformation()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            _logger.LogInformation($"Received get user inf by id {userId}");
            var foundUser = _userServices.GetUserById(userId);
            if (foundUser is null)
                return NotFound("User not found");
            var response = new UserDetailedInformationResponse(foundUser);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdateRequest request)
        {
            // get userId from claim
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            _logger.LogInformation($"Received to token with id {userId}");
            var updateUser = _userServices.UpdateUser(userId, request.FirstName, request.LastName,
                request.StudentIdentification,
                request.Password, request.Email);
            if (updateUser is null) return NotFound();
            return Ok(new UserInformationResponse(updateUser));
        }

        [HttpPut]
        [Route("avatar")]
        public IActionResult UpdateUserAvatar(IFormFile image)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            Console.WriteLine($"Receiving change avatar request from {userId}");
            // validate 
            if (image is null) return BadRequest("Empty file");
            if (image.Length <= 0) return BadRequest("Empty file");
            _logger.LogInformation($"Received file with content type {image.ContentType}");
            var allowedContentType = new List<string>
            {
                "image/jpg",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (!allowedContentType.Contains(image.ContentType.ToLower())){
                _logger.LogError("Throw bad request because of insufficent content type");
                return BadRequest("Content type is not image");
            }
                

            var fileExtension = Path.GetExtension(image.FileName);

            _logger.LogInformation($"Received file with extension {fileExtension}");
            // try upload the file 
            string resultUploadUrl;
            using (var stream = image.OpenReadStream())
            {
                resultUploadUrl = _userServices.UpdateUserAvatar(userId, stream);
            }

            if (string.IsNullOrEmpty(resultUploadUrl))
                return StatusCode(500, new {Error = "Error happened while uploading picture to cloud"});

            return Ok(new {ProfilePictureUrl = resultUploadUrl});
        }
    }
}