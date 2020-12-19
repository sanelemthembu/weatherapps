using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace weatherapp.Controllers
{
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private IHelper _helper;

        public RegisterController(IHelper helper)
        {
            _helper = helper;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var created = _helper.CreateUser(user);
            return Ok(created);
        }
    }
}