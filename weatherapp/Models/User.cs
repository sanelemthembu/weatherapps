using System;
using System.Text.Json.Serialization;

namespace weatherapp
{
    public class User
    {
        public DateTime dob { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
