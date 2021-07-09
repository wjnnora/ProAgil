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
    public class RedeSocialController : ControllerBase
    {
        private readonly IRedesSociaisRepository _redeSocialRepository;
        public RedeSocialController(IRedesSociaisRepository redeSocialRepository)
        {
            _redeSocialRepository = redeSocialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<RedeSocial> redesSociais = await _redeSocialRepository.GetAllRedesSociaisAsync();
                return Ok(redesSociais);
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
                RedeSocial redeSocial = await _redeSocialRepository.GetRedeSocialByIdAsync(id);
                if (redeSocial != null)
                {
                    return Ok(redeSocial);
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
                RedeSocial redeSocial = await _redeSocialRepository.GetRedeSocialByNomeAsync(nome);
                if (redeSocial != null)
                {
                    return Ok(redeSocial);
                }

                return NotFound();
            }
            catch (Exception)
            {
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(RedeSocial redeSocial)
        {
            try
            {
                _redeSocialRepository.Insert(redeSocial);
                if (await _redeSocialRepository.SaveChangesAsync())
                {
                    return Created($"api/redesocial/{redeSocial.Id}", redeSocial);
                }

                return BadRequest();
            }
            catch (Exception)
            {
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RedeSocial model)
        {
            try
            {
                if (await _redeSocialRepository.Exists(id))
                {
                    model.Id = id;
                    _redeSocialRepository.Update(model);

                    if (await _redeSocialRepository.SaveChangesAsync())
                    {
                        return Created($"api/redesocial/{model.Id}", model);
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
                RedeSocial redeSocial = await _redeSocialRepository.GetRedeSocialByIdAsync(id);
                if (redeSocial != null)
                {                    
                    _redeSocialRepository.Delete(redeSocial);

                    if (await _redeSocialRepository.SaveChangesAsync())
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