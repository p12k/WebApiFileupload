using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWEbAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeWEbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // [Route("login")]
        // [HttpPost(Name ="login")]
        //[HttpGet(Name = "login")]
        //public   Login Login(Login login)
        //{
        //    Guid g= Guid.NewGuid();//object
        //    var token = "";
        //    if (login.UserName.Equals("pooja") && login.Password.Equals("motumentalpagal"))
        //        token = g.ToString();
        //    else
        //        token = "Invalid username and Password";

        //    return new Login() { Token=token};

        //}
        [HttpPost(Name = "login")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            if (user.UserName == "motu" && user.Password == "pagal")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:4200",
                    audience: "https://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString });
            }

            return Unauthorized();
        }

    }
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet, Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}
