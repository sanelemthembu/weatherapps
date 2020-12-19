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
        void Insert(string data);
        bool CreateUser(User user);
        User GetUser(AuthenticateRequest login);
        User GetUserByusername(string username);
        List<User> GetUsers();
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

        public bool CreateUser(User user)
        {
            //Passwords will be hased in production
            string stm = $"Insert into users (username, name, password, dob) values ('{user.Username}','{user.Name}','P@ssword123','{DateTime.Now.ToString()}');";
            connection.Execute(stm);
            return true;
        }

        public User GetUserByusername(string username)
        {
            string stm = $"SELECT * from users where username = '{username}';";
            var user = connection.QueryFirstOrDefault<User>(stm);
            return user;
        }
        public User GetUser(AuthenticateRequest login)
        {
            string stm = $"SELECT * from users where username = '{login.Username}' and password = '{login.Password}';";
            var user = connection.QueryFirstOrDefault<User>(stm);
            return user;
        }
        public List<User> GetUsers()
        {
            string stm = $"SELECT * from users;";
            var user = connection.Query<User>(stm);            
            return user.AsList();
        }
    }
}
