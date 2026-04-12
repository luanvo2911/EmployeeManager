using AutoMapper;
using EmployeeManager.Common.Encryption;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Entity.Entities;
using EmployeeManager.WebAPI.Model;
using EmployeeManager.Common.ResourceUtils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeeManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class LoginController : Controller
    {

        private readonly IVLoginService _loginService;
        private readonly IMapper _automapper;
        private readonly IConfiguration _configuration;

        public LoginController(IVLoginService loginService, IMapper automapper, IConfiguration configuration)
        {
            _loginService = loginService;
            _automapper = automapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // check if the model state is valid before attempting to authenticate the user
            if (ModelState.IsValid)
            {
                // Authenticate the user using the login service
                Vlogin? authenticatedUser = _loginService.Login(request.username, request.password);
                LoginModel loginModel = _automapper.Map<LoginModel>(authenticatedUser);

                if (loginModel == null)
                {
                    return Unauthorized(Message.RES0004);
                }
                else
                {
                    // Generate JWT token for the authenticated user
                    var JWTKey = _configuration.GetSection("JsonWebToken:SecretKey").Value;
                    if (JWTKey == null)
                    {
                        throw new NullReferenceException(Message.RES0005);
                    }

                    var JWTByteKey = Encoding.ASCII.GetBytes(JWTKey);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(new[]
                        {
                            new System.Security.Claims.Claim("username", loginModel.userName),
                            new System.Security.Claims.Claim("passwords", loginModel.password),
                            new System.Security.Claims.Claim("email", loginModel.email),
                            new System.Security.Claims.Claim("userRole", loginModel.userRole)

                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(JWTByteKey), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

    public class LoginRequest
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
