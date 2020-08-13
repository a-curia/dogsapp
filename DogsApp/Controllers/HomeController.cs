using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogsApp.Controllers
{
    public class HomeController : ApiController
    {
        //[Authorize]
        public IActionResult Get()
        {
            return Ok("Works");
        }
    }
}
