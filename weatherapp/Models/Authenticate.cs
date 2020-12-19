using System.ComponentModel.DataAnnotations;

namespace weatherapp
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Name = user.Name;
            Username = user.Username;
            Token = token;
        }
    }
}
