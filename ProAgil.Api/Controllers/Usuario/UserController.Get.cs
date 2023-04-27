using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using ProAgil.Api.DTO.User;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Http;

namespace ProAgil.Api.Controllers.Usuario
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
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

        /// <summary>
        /// Retorna um usuário
        /// </summary>
        /// <param name="userDTO">Informações do usuário</param>
        /// <returns><see cref="UserDTO"/>Retorna um usuário</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public IActionResult Get([FromQuery] UserDTO userDTO)
        {
            return Ok(userDTO);
        }                
    }
}