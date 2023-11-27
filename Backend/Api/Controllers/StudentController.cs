using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHealthStudent")]
        public string GetHealth()
        {
            return $"Student ok @ {DateTime.Now.ToLocalTime()}";
        }
    }
}
