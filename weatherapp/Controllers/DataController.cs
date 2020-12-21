using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace weatherapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IDataSetup _dataSetup;

        public DataController(
            ILogger<WeatherController> logger,
            IDataSetup dataSetup
            )
        {
            _logger = logger;
            _dataSetup = dataSetup;
        }

        [HttpGet]
        public IActionResult Data()
        {
            _dataSetup.ClearData();
            var generated = _dataSetup.GenerateData();
            if (generated)
            {
                return Ok(generated);
            }
            else
            {
                return BadRequest(generated);
            }
            
        }
    }
}
