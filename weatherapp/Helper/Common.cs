using Dapper;
using JWT.Controllers;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace weatherapp
{
    public interface IHelper
    {
        Task<IEnumerable<WeatherDetails>> GetWeatherForecast(string date);
        void Insert(string data);

        UserModel GetUser(TokenController.LoginModel login);
        bool CreateUser(UserModel user);
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

        public void Insert(string data)
        {
            connection.Execute(data);
        }

        public bool CreateUser(UserModel user)
        {
            string stm = $"Insert into users (username, name, password) values ('{user.Username}','{user.Name}','P@ssword123');";
            connection.Execute(stm);
            return true;
        }

        public UserModel GetUser(TokenController.LoginModel login)
        {
            string stm = $"SELECT * from users where username = '{login.Username}' and password = '{login.Password}';";
            var user = connection.QueryFirstOrDefault<UserModel>(stm);
            return user;
        }
    }
}
