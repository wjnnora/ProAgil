using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Domain;
using ProAgil.Api.DTO;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoteRepository _loteRepository;        

        public LoteController(IMapper mapper, ILoteRepository loteRepository)
        {
            _mapper = mapper;
            _loteRepository = loteRepository;            
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoteDTO loteDTO)
        {
            try
            {
                var lote = _mapper.Map<Lote>(loteDTO);
                lote = await _loteRepository.Insert(lote);
                _mapper.Map(lote, loteDTO);             
                
                return Created($"api/lote/{loteDTO.Id}", loteDTO); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] LoteDTO loteDTO)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdAsync(id);

                if (lote is null)
                    return NotFound();

                _mapper.Map(loteDTO, lote);
                lote = await _loteRepository.Update(lote);
                _mapper.Map(lote, loteDTO);

                return Created($"api/evento/{loteDTO.Id}", loteDTO);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdAsync(id);

                if (lote is null)
                    return NotFound();

                await _loteRepository.Delete(lote);
                
                return NoContent();                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}