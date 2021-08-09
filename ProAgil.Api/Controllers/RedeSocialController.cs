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
    public class RedeSocialController : ControllerBase
    {
        private readonly IRedesSociaisRepository _redeSocialRepository;
        private readonly IMapper _mapper;
        public RedeSocialController(IRedesSociaisRepository redeSocialRepository, IMapper mapper)
        {
            _redeSocialRepository = redeSocialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<RedeSocial> redesSociais = await _redeSocialRepository.GetAllRedesSociaisAsync();
                IEnumerable<RedeSocialResponse> redesSociaisResponse = _mapper.Map<IEnumerable<RedeSocialResponse>>(redesSociais);
                return Ok(redesSociaisResponse);
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
                RedeSocialResponse redeSocialResponse = _mapper.Map<RedeSocialResponse>(redeSocial);
                if (redeSocialResponse != null)
                {
                    return Ok(redeSocialResponse);
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
                RedeSocialResponse redeSocialResponse = _mapper.Map<RedeSocialResponse>(redeSocial);
                if (redeSocialResponse != null)
                {
                    return Ok(redeSocialResponse);
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