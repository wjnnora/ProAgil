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
    public class PalestranteController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPalestranteRepository _palestranteRepository;

        public PalestranteController(IMapper mapper, IPalestranteRepository palestranteRepository)
        {
            _mapper = mapper;
            _palestranteRepository = palestranteRepository;
        }

        [HttpGet]
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

        [HttpGet("{id}")]        
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

        [HttpGet("getByName/{nome}")]            
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PalestranteDTO palestranteDTO)
        {
            try
            {
                var palestrante = _mapper.Map<Palestrante>(palestranteDTO);
                palestrante = await _palestranteRepository.Insert(palestrante);
                _mapper.Map(palestrante, palestranteDTO);         
                
                return Created($"api/palestrante/{palestranteDTO.Id}", palestranteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PalestranteDTO palestranteDTO)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);

                if (palestrante is null)
                    return NotFound();

                _mapper.Map(palestranteDTO, palestrante);
                palestrante = await _palestranteRepository.Update(palestrante);
                _mapper.Map(palestrante, palestranteDTO);

                return Created($"/api/palestrante/{palestranteDTO.Id}", palestranteDTO);                
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
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);

                if (palestrante is null)
                    return NotFound();

                await _palestranteRepository.Delete(palestrante);

                return NoContent();                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}