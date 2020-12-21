using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace weatherapp
{
    public interface IDataSetup
    {
        bool ClearData();
        bool GenerateData();
    }

    public class DataSetup : IDataSetup
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

        public bool CreateUser(User user)
        {
            string stm = $"Insert into users (username, name, password, dob) values ('{user.Username}','{user.Name}','P@ssword123','{DateTime.Now.ToString()}');";
            connection.Execute(stm);
            return true;
        }
        public bool ClearData()
        {
            string stm = $"delete from weatherforecast;";
            return connection.Execute(stm) == 1;
        }

        public bool GenerateData()
        {
            var rng = new Random();
            var today = DateTime.Now;
            DateTime t = DateTime.Parse($"2020/12/{today.Date.Day} 11:00:00 PM");

            var loopTime = 0;

            while (loopTime < 16)
            {
                for (int i = 0; i < 24; i++)
                {
                    var temp = rng.Next(10, 38);
                    var minTemp = rng.Next(-5, 15);
                    var desc = getDesc(temp);
                    var data = $"INSERT INTO weatherforecast(description, temp, temp_min, temp_max, forecast_datetime) VALUES ('{desc}', {temp}, {minTemp}, {temp}, '{t}');";
                    connection.Execute(data);

                    t = t.AddHours(-1);
                }
                t.AddDays(-1);
                loopTime++;
            }
            return true;
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