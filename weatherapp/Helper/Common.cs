using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace weatherapp
{
    public interface IHelper
    {
        Task<IEnumerable<WeatherDetails>> GetWeatherForecast(string date);
    }

    public class Helper : IHelper
    {
        private readonly SqliteConnection connection = new SqliteConnection("Data Source=test.db");

        public async Task<IEnumerable<WeatherDetails>> GetWeatherForecast(string date)
        {
            string yesterday;
            string today;

            if (!string.IsNullOrEmpty(date))
            {
                var formatedDate = DateTime.Parse(date);
                yesterday = formatedDate.AddDays(-1).ToString("yyyy/MM/d");
                today = formatedDate.ToString("yyyy/MM/d"); ;
            }
            else
            {
                yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/d");
                today = DateTime.Now.ToString("yyyy/MM/d");
            }
            var qu = $"SELECT* FROM weatherforecast WHERE forecast_datetime like '%{today}%'  or forecast_datetime like '%{yesterday}%'";            
            var res = await connection.QueryAsync<WeatherDetails>(qu);
            return res;
        }
    }
}
