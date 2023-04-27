using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Api.DTO;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers.Eventos
{    
    [ApiController]
    [Route("api/[controller]")]
    public partial class EventoController : ControllerBase
    {
        private IMapper _mapper;
        private IEventoRepository _eventoRepository;

        public EventoController(IMapper mapper, IEventoRepository eventoRepository)
        {
            _mapper = mapper;
            _eventoRepository = eventoRepository;
        }

        /// <summary>
        /// Retorna todos os eventos
        /// </summary>
        /// <returns><see cref="EventoDTO"/>Lista de eventos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(EventoDTO), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync();
                var eventosDTO = _mapper.Map<IEnumerable<EventoDTO>>(eventos);

                return Ok(eventosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor");
            }
        }

        /// <summary>
        /// Retorna um evento de acordo com o Id informado
        /// </summary>
        /// <param name="id">Id do evento a ser retornado</param>
        /// <returns><see cref="EventoDTO"/>Evento do Id informado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventoDTO), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);

                if (evento is null)
                    return NotFound();

                var eventoDTO = _mapper.Map<EventoDTO>(evento);

                return Ok(eventoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        /// <summary>
        /// Retorna um evento de acordo com o tema informado
        /// </summary>
        /// <param name="tema"></param>
        /// <returns><see cref="EventoDTO"/>Evento com o tema informado</returns>
        [HttpGet("getByTema/{tema}")]
        [ProducesResponseType(typeof(EventoDTO), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get([FromRoute] string tema)
        {
            try
            {
                var evento = await _eventoRepository.GetEventosByTemaAsync(tema);

                if (evento is null)
                    return NotFound();

                var eventoDTO = _mapper.Map<EventoDTO>(evento);

                return Ok(eventoDTO);                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }                              
    }
}
