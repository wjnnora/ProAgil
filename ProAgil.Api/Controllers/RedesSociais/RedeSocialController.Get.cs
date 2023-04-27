using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Api.DTO;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers.RedesSociais
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class RedeSocialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRedesSociaisRepository _redeSocialRepository;

        public RedeSocialController(IMapper mapper, IRedesSociaisRepository redeSocialRepository)
        {
            _mapper = mapper;
            _redeSocialRepository = redeSocialRepository;
        }

        /// <summary>
        /// Retorna todas as Redes Sociais cadastradas
        /// </summary>
        /// <returns><see cref="RedeSocialDTO"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(RedeSocialDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var redesSociais = await _redeSocialRepository.GetAllRedesSociaisAsync();
                var redesSociaisDTO = _mapper.Map<IEnumerable<RedeSocialDTO>>(redesSociais);

                return Ok(redesSociaisDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Retorna uma Rede Social
        /// </summary>
        /// <param name="id">Id da Rede Social</param>
        /// <returns><see cref="RedeSocialDTO"/>Retorna a Rede Social do Id informado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RedeSocialDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialByIdAsync(id);

                if (redeSocial is null)
                    return NotFound();

                var redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);

                return Ok(redeSocialDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Retorna uma Rede Social
        /// </summary>
        /// <param name="nome">Nome da Rede Social</param>
        /// <returns><see cref="RedeSocialDTO"/>Retorna a Rede Social com o nome informado</returns>
        [HttpGet("getByName/{nome}")]
        [ProducesResponseType(typeof(RedeSocialDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] string nome)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialByNomeAsync(nome);

                if (redeSocial is null)
                    return NotFound();

                var redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);

                return Ok(redeSocialDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }                
    }
}