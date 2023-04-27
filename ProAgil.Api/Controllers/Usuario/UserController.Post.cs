using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

using ProAgil.Api.DTO.User;
using ProAgil.Domain.Identity;

namespace ProAgil.Api.Controllers.Usuario
{
    public partial class UserController : ControllerBase
    {
        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="userDTO">Informações do novo usuário</param>
        /// <returns><see cref="UserDTO"/>Retorna o novo usuário criado</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Realiza o Login de um usuário
        /// </summary>
        /// <param name="userLoginDTO">Informações de Login</param>
        /// <returns><see cref="UserDTO"/>Retorna o usuário logado</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
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

        #region Privado
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
        #endregion
    }
}
