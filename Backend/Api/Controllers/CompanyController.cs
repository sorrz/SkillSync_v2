using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHealthCompany")]
        public string GetHealth()
        {
            return $"Company ok @ {DateTime.Now.ToLocalTime()}";
        }
    }
}
