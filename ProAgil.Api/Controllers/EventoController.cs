using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;        
        private IEventoRepository _eventoRepository;

        public EventoController(ILogger<EventoController> logger, IEventoRepository eventoRepository)
        {
            _logger = logger;
            _eventoRepository = eventoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultado = await _eventoRepository.GetAllEventosAsync();
                return Ok(resultado);
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
                var result = await _eventoRepository.GetEventoByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _eventoRepository.GetEventosByTemaAsync(tema);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
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
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
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
                        return Created($"/api/evento/{evento.Id}", evento);
                    }

                    throw new Exception();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
