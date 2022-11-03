using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Todo3.Data;
using Todo3.ModelView;

namespace Todo3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Todo3Context _context;
        private IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        public UsersController(Todo3Context context,IConfiguration config)//,SignInManager<User> signInManager)
        {
            _context = context;
            _config = config;
           // _signInManager = signInManager;
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterUser registerUser)
        {
            if(ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = registerUser.Email,
                    Password = registerUser.Password
                };
                _context.Add(user);
                _context.SaveChanges();
                return Ok("User Created Successfuly");
            }
            return BadRequest("Data is Not Valid");
        }
        [HttpPost("Login")]
        public IActionResult Login(RegisterUser registerUser)
        {
            var user = _context.User.SingleOrDefault(x =>( x.Email == registerUser.Email && x.Password == registerUser.Password));
            if (user != null)
            {


                return Ok(GenerateJSONWebToken(user));
            }
            return BadRequest("User Not Exist");



        }

        [HttpPut("{Password}")]
        //[Authorize]
        public IActionResult ChangePasswors(string Password,RegisterUser registerUser)
        {
            var user = _context.User.SingleOrDefault(x => x.Email == registerUser.Email && x.Password == registerUser.Password);
            if (user != null)
            {
                user.Password = Password;
                _context.Update(user);
                _context.SaveChanges();
                return Ok();
                
            }
            else
              return BadRequest("User Not Exist");
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
