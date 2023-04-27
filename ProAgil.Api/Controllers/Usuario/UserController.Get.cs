using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using ProAgil.Api.DTO.User;
using ProAgil.Domain.Identity;

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

        [HttpGet]
        public IActionResult Get([FromQuery] UserDTO userDTO)
        {
            return Ok(userDTO);
        }                
    }
}