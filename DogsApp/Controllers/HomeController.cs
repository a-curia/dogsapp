using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogsApp.Controllers
{
    public class HomeController : ApiController
    {
        //[Authorize]
        public ActionResult Get()
        {
            return Ok("Works");
        }
    }
}
