using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Api.DTO;
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
        private readonly IMapper _mapper;
        public LoteController(ILoteRepository loteRepository, IEventoRepository eventoRepository, IMapper mapper)
        {
            _loteRepository = loteRepository;
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Lote> lotes = await _loteRepository.GetAllLotesAsync();
                IEnumerable<LoteDTO> lotesDTO = _mapper.Map<IEnumerable<LoteDTO>>(lotes);
                return Ok(lotesDTO);
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
                LoteDTO loteDTO = _mapper.Map<LoteDTO>(lote);
                if (loteDTO != null)
                {
                    return Ok(loteDTO);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoteDTO loteDTO)
        {
            try
            {
                Lote lote = _mapper.Map<Lote>(loteDTO);
                _loteRepository.Insert(lote);

                if (await _loteRepository.SaveChangesAsync())
                {
                    lote = await _loteRepository.GetLastLoteInserted();
                    return Created($"api/lote/{lote.Id}", lote);
                }

                return BadRequest();    
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LoteDTO loteDTO)
        {
            try
            {
                if (await _loteRepository.Exists(id))
                {
                    loteDTO.Id = id;
                    Lote lote = _mapper.Map<Lote>(loteDTO);
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