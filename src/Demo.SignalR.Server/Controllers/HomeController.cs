using Microsoft.AspNetCore.Mvc;

namespace Demo.SignalR.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(System.Environment.MachineName);
        }
    }
}