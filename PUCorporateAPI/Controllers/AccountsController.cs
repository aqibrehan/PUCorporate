using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PUCorporate.Model.Model;
using PUCorporate.Model.Model.ViewModel;
using PUCorporate.Model.DTO;
using PUCorporate.DataAccessLayer.Services.Interfaces;
using PUCorporate.CommonHelper;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cors;

namespace PUCorporateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthData _authService;
        private readonly IMail _mailServices;
        private readonly IConfiguration _configuration;
        public AccountsController(IAuthData authService, IConfiguration configuration, IMail mailServices)
        {
            _authService = authService;
            _configuration = configuration;
            _mailServices = mailServices;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromForm]RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not valid");
            }
            User model = new User()
            {

                LoginName = registerDTO?.Loginname,
                Password = registerDTO?.Password,
               // ConfirmPassword = registerDTO?.ConfirmPassword,
            };
            await _authService.CreateAdminandUser(model);
            return Ok(new { message = "Admin Register Successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.username)  & string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return Content("Email or Password required!");
            }
            var jwt = GenerateJwt(loginDTO.username.ToString(), loginDTO.Token);

            User user = new ()
            {
            LoginName=loginDTO.username,
            Password=loginDTO.Password,
            Token= loginDTO.Token,
            Jwt=jwt
            };

            //loginDTO.Jwt= jwt;
            //loginDTO.Jwt = UserToken.Token;

            var result = await _authService.GetDataforAuth(user);

            if (result == null)
            {
                return Unauthorized("Invalid credentials.");
            }


            LoginViewModel loginModel = new()
            {
                EmailAddress = result.Email,
                FirstName = result.FirstName,
                IsAdmin = result.IsAdmin,
                LoginID = result.LoginID,
                LoginName = result.LoginName,
                LastName = result.LastName,
                jwtToken = jwt,
            
            };
            return Ok(loginModel);
        }

        internal string GenerateJwt(string Email, string Token)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[] {
            //        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetValue<string>("Jwt:Subject")),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            //        new Claim(ClaimTypes.Sid, UserId),
            //        new Claim(JwtRegisteredClaimNames.Email, Email),

            //};
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("username", Email));
            claims.Add(new Claim("token", Token));
            claims.Add(new Claim("passwordEncKey", "SQL SERVER 2008 APEX"));

            var token = new JwtSecurityToken(_configuration.GetValue<string>("Jwt:Issuer"),
              _configuration.GetValue<string>("Jwt:Audiance"),
              claims,
              expires: DateTime.Now.AddHours(3),
              signingCredentials: credentials);


            string data = new JwtSecurityTokenHandler().WriteToken(token);
            return data;
        }
    }
}
