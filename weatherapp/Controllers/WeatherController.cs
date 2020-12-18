using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace weatherapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IHelper _helper;

        public WeatherController(
            ILogger<WeatherController> logger,
            IHelper helper)
        {
            _logger = logger;
            _helper = helper;
        }

        [HttpGet, Authorize]
        public Root Get()
        {
            var forecast = _helper.GetWeatherForecast("").GetAwaiter().GetResult();
            return new Root
            {
                list = forecast.ToList()
            };
        }

        [HttpGet("{date}"), Authorize]
        public Root GetById(string date)
        {
            var forecast = _helper.GetWeatherForecast(date).GetAwaiter().GetResult();
            return new Root
            {
                list = forecast.ToList()
            };
        }
    }
}
