using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Api.DTO;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers.Lotes
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class LoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoteRepository _loteRepository;

        public LoteController(IMapper mapper, ILoteRepository loteRepository)
        {
            _mapper = mapper;
            _loteRepository = loteRepository;
        }

        /// <summary>
        /// Retorna todos os lotes cadastrados
        /// </summary>
        /// <returns><see cref="LoteDTO"/>Lista de lotes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(LoteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lotes = await _loteRepository.GetAllLotesAsync();
                var lotesDTO = _mapper.Map<IEnumerable<LoteDTO>>(lotes);

                return Ok(lotesDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Retorna o lote do Id informado
        /// </summary>
        /// <param name="id">Id do lote</param>
        /// <returns><see cref="LoteDTO"/>Lote do Id informado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LoteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdAsync(id);

                if (lote is null)
                    return NotFound();

                var loteDTO = _mapper.Map<LoteDTO>(lote);

                return Ok(loteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}