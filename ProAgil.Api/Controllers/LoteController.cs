using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteController: ControllerBase
    {
        private readonly ILoteRepository _loteRepository;
        private readonly IEventoRepository _eventoRepository;
        public LoteController(ILoteRepository loteRepository, IEventoRepository eventoRepository)
        {
            _loteRepository = loteRepository;
            _eventoRepository = eventoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Lote> lotes = await _loteRepository.GetAllLotesAsync();
                return Ok(lotes);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Lote lote = await _loteRepository.GetLoteByIdAsync(id);
                if (lote != null)
                {
                    return Ok(lote);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Lote lote)
        {
            try
            {
                if (lote?.EventoId != null)
                {
                    if (await _eventoRepository.Exists(lote.EventoId))
                    {
                        _loteRepository.Insert(lote);

                        if (await _loteRepository.SaveChangesAsync())
                        {
                            return Created($"api/lote/{lote.Id}", lote);
                        }
                    }

                    return BadRequest($"EventId {lote.EventoId} doest not exists.");
                }

                return BadRequest();    
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Lote lote)
        {
            try
            {
                if (await _loteRepository.Exists(id))
                {
                    lote.Id = id;
                    _loteRepository.Update(lote);

                    if (await _loteRepository.SaveChangesAsync())
                    {
                        return Created($"api/evento/{lote.Id}", lote);
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
                Lote lote = await _loteRepository.GetLoteByIdAsync(id);
                if (lote != null)
                {                    
                    _loteRepository.Delete(lote);

                    if (await _loteRepository.SaveChangesAsync())
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