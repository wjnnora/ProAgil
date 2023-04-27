using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Api.DTO;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers.Palestrantes
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class PalestranteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPalestranteRepository _palestranteRepository;

        public PalestranteController(IMapper mapper, IPalestranteRepository palestranteRepository)
        {
            _mapper = mapper;
            _palestranteRepository = palestranteRepository;
        }

        /// <summary>
        /// Retorna todos os palestrantes cadastrados
        /// </summary>
        /// <returns><see cref="PalestranteDTO"/>Lista de palestrantes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PalestranteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var palestrantes = await _palestranteRepository.GetAllPalestrantesAsync();
                var palestrantesDTO = _mapper.Map<IEnumerable<PalestranteDTO>>(palestrantes);

                return Ok(palestrantesDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Retorna um palestrante
        /// </summary>
        /// <param name="id">Id do palestrante</param>
        /// <returns><see cref="PalestranteDTO"/>Palestrante do Id informado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PalestranteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);

                if (palestrante is null)
                    return NotFound();

                var palestranteDTO = _mapper.Map<PalestranteDTO>(palestrante);

                return Ok(palestranteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Retorna o palestrante com o nome informado
        /// </summary>
        /// <param name="nome">Nome do palestrante</param>
        /// <returns><see cref="PalestranteDTO"/>Palestrante com o nome informado</returns>
        [HttpGet("getByName/{nome}")]
        [ProducesResponseType(typeof(PalestranteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] string nome)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByNomeAsync(nome);

                if (palestrante is null)
                    return NotFound();

                var palestranteDTO = _mapper.Map<PalestranteDTO>(palestrante);

                return Ok(palestranteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }                       
    }
}