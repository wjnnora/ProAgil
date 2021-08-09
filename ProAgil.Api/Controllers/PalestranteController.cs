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
    public class PalestranteController: ControllerBase
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IMapper _mapper;
        public PalestranteController(IPalestranteRepository palestranteRepository, IMapper mapper)
        {
            _palestranteRepository = palestranteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Palestrante> palestrantes = await _palestranteRepository.GetAllPalestrantesAsync();
                IEnumerable<PalestranteDTO> palestrantesDTO = _mapper.Map<IEnumerable<PalestranteDTO>>(palestrantes);
                return Ok(palestrantesDTO);
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
                Palestrante palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);
                PalestranteDTO palestranteDTO = _mapper.Map<PalestranteDTO>(palestrante);
                if (palestranteDTO != null)
                {
                    return Ok(palestranteDTO);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpGet("getByName/{nome}")]            
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                Palestrante palestrante = await _palestranteRepository.GetPalestranteByNomeAsync(nome);
                PalestranteDTO palestranteDTO = _mapper.Map<PalestranteDTO>(palestrante);
                if (palestranteDTO != null)
                {
                    return Ok(palestranteDTO);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante palestrante)
        {
            try
            {
                _palestranteRepository.Insert(palestrante);

                if (await _palestranteRepository.SaveChangesAsync())
                {
                    return Created($"api/palestrante/{palestrante.Id}", palestrante);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Palestrante palestrante)
        {
            try
            {
                if (await _palestranteRepository.Exists(id))
                {
                    _palestranteRepository.Update(palestrante);

                    if (await _palestranteRepository.SaveChangesAsync())
                    {
                        return Created($"/api/palestrante/{palestrante.Id}", palestrante);
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
                Palestrante palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);
                if (palestrante != null)
                {
                    _palestranteRepository.Delete(palestrante);
                    if (await _palestranteRepository.SaveChangesAsync())
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