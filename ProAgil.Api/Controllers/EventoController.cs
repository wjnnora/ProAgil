using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Api.DTO;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {        
        private IEventoRepository _eventoRepository;
        private IMapper _mapper;

        public EventoController(IEventoRepository eventoRepository, IMapper mapper)
        {   
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Evento> eventos = await _eventoRepository.GetAllEventosAsync();
                IEnumerable<EventoDTO> eventosDTO = _mapper.Map<IEnumerable<EventoDTO>>(eventos);
                return Ok(eventosDTO);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Evento evento = await _eventoRepository.GetEventoByIdAsync(id);
                EventoDTO eventoDTO = _mapper.Map<EventoDTO>(evento);
                return Ok(eventoDTO);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpGet("getByTema/{tema}")]            
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                Evento evento = await _eventoRepository.GetEventosByTemaAsync(tema);
                EventoDTO eventoDTO = _mapper.Map<EventoDTO>(evento);
                if (eventoDTO != null)
                {
                    return Ok(eventoDTO);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO eventoDTO)
        {
            try
            {
                Evento evento = _mapper.Map<Evento>(eventoDTO);
                evento = await _eventoRepository.Insert(evento);
                _mapper.Map(evento, eventoDTO);
                return Created($"/api/evento/{eventoDTO.Id}", eventoDTO);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDTO eventoDTO)
        {
            try
            {
                Evento evento = await _eventoRepository.GetEventoByIdAsync(id);
                if (evento != null)
                {
                    _mapper.Map(eventoDTO, evento);
                    evento = await _eventoRepository.Update(evento);
                    _mapper.Map(evento, eventoDTO);   

                    return Created($"/api/evento/{eventoDTO.Id}", eventoDTO);                 
                }

                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Evento evento = await _eventoRepository.GetEventoByIdAsync(id);
                if (evento != null)
                {
                    if (await _eventoRepository.Delete(evento))
                    {
                        return NoContent();                        
                    }

                    throw new Exception();
                }

                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
