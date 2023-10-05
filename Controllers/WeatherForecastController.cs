using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HvZ_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public ActionResult GetPublic()
        {
            return Ok(new { Message = "Public resource" });
        }

        [HttpGet("protected")]
        public ActionResult GetProtected()
        {
            return Ok(new { Message = "Protected resource" });
        }

        [HttpGet("role")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult GetRoles()
        {
            return Ok(new { Message = "Roles resource" });
        }

        [HttpGet("subject")]
        public ActionResult GetSubject()
        {
            var subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(new { Subject = subject });
        }

    }
}