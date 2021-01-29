using Microsoft.AspNetCore.Mvc;
using RPIFun.API.Services;
using RPIFun.Core;

namespace RPIFun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        public readonly EnvironmentService _environmentService;

        public EnvironmentController(EnvironmentService environmentService)
        {
            _environmentService = environmentService;
        }

        [HttpGet]
        public EnvResult Get()
        {
            return _environmentService.GetEnvironment();
        }
    }
}
