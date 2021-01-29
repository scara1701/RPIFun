using Microsoft.AspNetCore.Mvc;
using RPIFun.API.Services;

namespace RPIFun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LEDController : ControllerBase
    {
        private readonly LEDService _LEDService;
        public LEDController(LEDService LEDService)
        {
            _LEDService = LEDService;
        }

        [HttpGet]
        public bool Get()
        {
            return _LEDService.IsLEDON();
        }

        [Route("[action]")]
        [HttpGet]
        public bool Switch()
        {
            return _LEDService.HitLEDSwitch();
        }
    }
}
