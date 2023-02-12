using Microsoft.AspNetCore.Mvc;

namespace GameRemoteServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("Hi");
        }
    }
}