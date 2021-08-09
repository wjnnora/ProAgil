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
                IEnumerable<EventoResponse> eventosResponse = _mapper.Map<IEnumerable<EventoResponse>>(eventos);
                return Ok(eventosResponse);
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
                EventoResponse eventoResponse = _mapper.Map<EventoResponse>(evento);
                return Ok(eventoResponse);
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
                EventoResponse eventoResponse = _mapper.Map<EventoResponse>(evento);
                if (eventoResponse != null)
                {
                    return Ok(eventoResponse);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocurreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                _eventoRepository.Insert(evento);

                if (await _eventoRepository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", evento);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento evento)
        {
            try
            {
                if (await _eventoRepository.Exists(id))
                {
                    _eventoRepository.Update(evento);

                    if (await _eventoRepository.SaveChangesAsync())
                    {
                        return Created($"/api/evento/{evento.Id}", evento);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Evento evento = await _eventoRepository.GetEventoByIdAsync(id);
                if (evento != null)
                {
                    _eventoRepository.Delete(evento);

                    if (await _eventoRepository.SaveChangesAsync())
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
