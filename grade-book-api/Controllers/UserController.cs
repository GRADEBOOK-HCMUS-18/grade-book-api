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
        private readonly ICloudPhotoHandler _cloudPhotoHandler;

        public UserController(ILogger<UserController> logger, IUserServices userServices, ICloudPhotoHandler cloudPhotoHandler)
        {
            _logger = logger;
            _userServices = userServices;
            _cloudPhotoHandler = cloudPhotoHandler;
        }

        [HttpGet]
        public IActionResult GetUserInformation()
        {
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            return BadRequest();
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
        [AllowAnonymous]
        [Route("avatar")]
        public IActionResult UpdateUserAvatar(IFormFile file)
        {
            
            //var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
            // validate 
            if (file is null) return BadRequest("Empty file");
            if (file.Length <= 0) return BadRequest("Empty file");
            _logger.LogInformation($"Received file with content type {file.ContentType}");
            var allowedContentType = new List<string>
            {
                "image/jpg",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (!allowedContentType.Contains(file.ContentType.ToLower()))
                return BadRequest("Content type is not image");

            var fileExtension = Path.GetExtension(file.FileName);

            _logger.LogInformation($"Received file with extension {fileExtension}");
            // try upload the file 
            string resultUploadUrl;
            using (Stream stream = file.OpenReadStream())
            {
                resultUploadUrl =  _cloudPhotoHandler.Upload(stream);    
                
            }

            if (string.IsNullOrEmpty(resultUploadUrl))
            {
                return StatusCode(500,new {Error = "Error happened while uploading picture to cloud"});
            }

            return Ok( new { ProfilePictureUrl = resultUploadUrl});
        }
    }
}