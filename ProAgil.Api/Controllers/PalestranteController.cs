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
    public class PalestranteController: ControllerBase
    {
        private readonly IPalestranteRepository _palestranteRepository;
        public PalestranteController(IPalestranteRepository palestranteRepository)
        {
            _palestranteRepository = palestranteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Palestrante> palestrantes = await _palestranteRepository.GetAllPalestrantesAsync();
                return Ok(palestrantes);
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
                if (palestrante != null)
                {
                    return Ok(palestrante);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpGet]    
        [Route("api/evento/getByName/{nome}")]    
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                Palestrante palestrante = await _palestranteRepository.GetPalestranteByNomeAsync(nome);
                if (palestrante != null)
                {
                    return Ok(palestrante);
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Palestrante model)
        {
            try
            {
                if (await _palestranteRepository.Exists(id))
                {
                    _palestranteRepository.Update(model);

                    if (await _palestranteRepository.SaveChangesAsync())
                    {
                        return Created($"/api/palestrante/{model.Id}", model);
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