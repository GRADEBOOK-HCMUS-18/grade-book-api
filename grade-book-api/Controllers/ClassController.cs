using System.Linq;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using grade_book_api.Requests.ClassRequests;
using grade_book_api.Responses.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace grade_book_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClassController: ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IClassService _classService;

        public ClassController(ILogger<ClassController> logger, IClassService classService)
        {
            _logger = logger;
            _classService = classService;
        }
        [HttpGet]
        public IActionResult GetClassList()
        {
            return Ok();
        }

        [HttpGet]
        [Route("classId")]
        public IActionResult GetClassDetail(int classId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddNewClass(AddNewClassRequest request)
        {
            
            var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "ID").Value);
          
            // create new class

            Class newAddClass = _classService.AddNewClass(request.Name, request.StartDate, request.Room,
                request.Description, userId);
            
            return Ok(new ClassShortInformationResponse(newAddClass));
        }
    }
}