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
                IEnumerable<RedeSocialDTO> redesSociaisDTO = _mapper.Map<IEnumerable<RedeSocialDTO>>(redesSociais);
                return Ok(redesSociaisDTO);
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
                RedeSocialDTO redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);
                if (redeSocialDTO != null)
                {
                    return Ok(redeSocialDTO);
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
                RedeSocialDTO redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);
                if (redeSocialDTO != null)
                {
                    return Ok(redeSocialDTO);
                }

                return NotFound();
            }
            catch (Exception)
            {
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(RedeSocialDTO redeSocialDTO)
        {
            try
            {
                RedeSocial redeSocial = _mapper.Map<RedeSocial>(redeSocialDTO);
                redeSocial = await _redeSocialRepository.Insert(redeSocial);
                redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);

                if (redeSocialDTO != null)
                {
                    return Created($"api/redesocial/{redeSocialDTO.Id}", redeSocialDTO);
                }

                return BadRequest();
            }
            catch (Exception)
            {
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RedeSocialDTO redeSocialDTO)
        {
            try
            {
                if (await _redeSocialRepository.Exists(id))
                {
                    redeSocialDTO.Id = id;
                    RedeSocial redeSocial = _mapper.Map<RedeSocial>(redeSocialDTO);
                    redeSocial = await _redeSocialRepository.Update(redeSocial);
                    redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);

                    if (redeSocialDTO != null)
                    {
                        return Created($"api/redesocial/{redeSocialDTO.Id}", redeSocialDTO);
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
                    if (await _redeSocialRepository.Delete(redeSocial))
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