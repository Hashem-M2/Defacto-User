
using Application.Service.User;
using Context;
using DTO_s.ViewResult;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IuserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IuserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] UserRegisterDTO User)
        {
            var usr = await _userService.Registration(User, "user");
            if (!usr.IsSuccess)
            {
                return BadRequest(usr.Message);
            }
                return Ok(usr.Message);
        }

      // [Route("addNewUser/{roleName}")]
       // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> addNewUser(UserRegisterDTO User, string? roleName)
        {
            var usr = await _userService.Registration(User, roleName);
            if (!usr.IsSuccess)
            {
                return BadRequest(usr.Message);
            }
            return Ok(usr.Message);

        }
  
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var usr = await _userService.LoginAsync(userDto);

                if(usr.IsSuccess == true)
                {
                    var Claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Email, usr.Entity.Email),
                         new Claim(ClaimTypes.Name, usr.Entity.UserName),
                         new Claim(JwtRegisteredClaimNames.Jti, usr.Entity.Id),
                         new Claim(ClaimTypes.Role,  usr.Entity.Role.ToString())
                    };
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]));

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: _configuration["jwt:Issuer"],
                        audience: _configuration["jwt:Audiences"],
                        expires: DateTime.Now.AddDays(50), 
                        claims: Claims,
                        signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
                        );

                    var StringToken = new JwtSecurityTokenHandler(  ).WriteToken(token);
                    return Ok(new { StringToken, Expire = token.ValidTo });
                }
                return Unauthorized(ModelState);

            }
            return Unauthorized(ModelState);
        }
    
    
    }
}


