using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Dtos;
using Model.Entities;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Accountant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this._repo = repo;
            this._config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            try
            {
                userDto.Dni = userDto.Dni;
                if (await _repo.UserExist(userDto.Dni))
                    return BadRequest("User already exists");

                User userToCreate = new User
                {
                    Name = userDto.Name,
                    LastName = userDto.LastName,
                    Dni = userDto.Dni,
                    Email = userDto.Email,
                    LastLogin = DateTime.Now
                };

                var createdUser = await _repo.Register(userToCreate, userDto.Password);

                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500,"Registration failed.");
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            try
            {
                User user = await _repo.Login(userDto.Dni, userDto.Password);

                if (user == null)
                    return Unauthorized();

                //Adding information to the token.
                var claims = new[]
                {
                    //nameId
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    //unique_name
                    new Claim(ClaimTypes.Name, user.Dni)
                };

                //Reading and signing the token signature from appsettings.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = credentials
                };

                //Creates a handler then creates a token with the token description.
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            catch (Exception)
            {
                return StatusCode(500, "Login failed.");
            }

        }
    }
}
