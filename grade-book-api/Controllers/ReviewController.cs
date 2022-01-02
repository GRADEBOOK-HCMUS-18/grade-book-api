using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grade_book_api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ReviewController: ControllerBase
    {
        private readonly IUserServices _userServices;

        public ReviewController(IUserServices userServices)
        {
            _userServices = userServices;
        }
    }
}