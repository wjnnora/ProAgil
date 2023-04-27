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

        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);
                var eventoDTO = _mapper.Map<EventoDTO>(evento);

                return Ok(eventoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get([FromRoute] string tema)
        {
            try
            {
                var evento = await _eventoRepository.GetEventosByTemaAsync(tema);
                var eventoDTO = _mapper.Map<EventoDTO>(evento);

                if (eventoDTO is not null)
                    return Ok(eventoDTO);

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }                              
    }
}
