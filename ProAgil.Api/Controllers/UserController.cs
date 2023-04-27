using System;
using AutoMapper;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using ProAgil.Api.DTO.User;
using ProAgil.Domain.Identity;

namespace ProAgil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;        

        public UserController(IMapper mapper, IConfiguration config, UserManager<User> userManager)
        {
            _mapper = mapper;
            _config = config;
            _userManager = userManager;            
        }

        [HttpGet]
        public IActionResult Get([FromQuery] UserDTO userDTO) 
        {            
            return Ok(userDTO);
        }        


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UserDTO userDTO) 
        { 
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                userDTO = _mapper.Map<UserDTO>(user);                

                if (result.Succeeded)
                    return Created($"/api/user/", userDTO);

                return BadRequest(result.Errors);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO) 
        { 
            try
            {
                var user = await _userManager.FindByNameAsync(userLoginDTO.UserName);
                var correctPassword = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

                if (!correctPassword)
                    return Unauthorized();

                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userLoginDTO.UserName.ToUpper());
                var appUserToReturn = _mapper.Map<UserLoginDTO>(appUser);
                var userToken = new { token = GenerateJWT(appUser).Result, user = appUserToReturn };

                return Ok(userToken);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        private async Task<string> GenerateJWT(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);            
        }
    }
}