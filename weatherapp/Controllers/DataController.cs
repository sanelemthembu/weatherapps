using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace weatherapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IHelper _helper;

        public DataController(
            ILogger<WeatherController> logger,
            IHelper helper
            )
        {
            _logger = logger;
            _helper = helper;
        }

        [HttpPost]
        public IActionResult Data()
        {
            var rng = new Random();
            var builder = new StringBuilder();
            DateTime t = DateTime.Parse("2020/12/18 11:00:00 PM");

            var loopTime = 0;

            while (loopTime < 16)
            {
                for (int i = 0; i < 24; i++)
                {
                    var temp = rng.Next(10, 38);
                    var minTemp = rng.Next(-5, 15);
                    var desc = getDesc(temp);
                    var data = $"INSERT INTO weatherforecast(description, temp, temp_min, temp_max, forecast_datetime) VALUES ('{desc}', {temp}, {minTemp}, {temp}, '{t}');";
                    _helper.Insert(data);                    

                    t = t.AddHours(-1);
                }
                t.AddDays(-1);
                loopTime++;
            }

            return Ok();
        }
        private static string getDesc(int temp)
        {
            var d = "Hot";
            if (temp < 15)
            {
                return "Cold";
            }
            if (temp < 23)
            {
                return "Cool";
            }
            return d;
        }
    }
}
