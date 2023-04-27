using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Api.DTO;
using ProAgil.Domain;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedeSocialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRedesSociaisRepository _redeSocialRepository;

        public RedeSocialController(IMapper mapper, IRedesSociaisRepository redeSocialRepository)
        {
            _mapper = mapper;
            _redeSocialRepository = redeSocialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var redesSociais = await _redeSocialRepository.GetAllRedesSociaisAsync();
                var redesSociaisDTO = _mapper.Map<IEnumerable<RedeSocialDTO>>(redesSociais);

                return Ok(redesSociaisDTO);
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
                var redeSocial = await _redeSocialRepository.GetRedeSocialByIdAsync(id);

                if (redeSocial is null)
                    return NotFound();

                var redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);

                return Ok(redeSocialDTO);                
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
                var redeSocial = await _redeSocialRepository.GetRedeSocialByNomeAsync(nome);

                if (redeSocial is null)
                    return NotFound();

                var redeSocialDTO = _mapper.Map<RedeSocialDTO>(redeSocial);

                return Ok(redeSocialDTO);
            }
            catch (Exception)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RedeSocialDTO redeSocialDTO)
        {
            try
            {
                var redeSocial = _mapper.Map<RedeSocial>(redeSocialDTO);
                redeSocial = await _redeSocialRepository.Insert(redeSocial);
                _mapper.Map(redeSocial, redeSocialDTO);

                return Created($"api/redesocial/{redeSocialDTO.Id}", redeSocialDTO);
            }
            catch (Exception)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] RedeSocialDTO redeSocialDTO)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialByIdAsync(id);

                if (redeSocial is null)
                    return NotFound();

                _mapper.Map(redeSocialDTO, redeSocial);
                redeSocial = await _redeSocialRepository.Update(redeSocial);
                _mapper.Map(redeSocial, redeSocialDTO);

                return Created($"api/redesocial/{redeSocialDTO.Id}", redeSocialDTO);                
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
                var redeSocial = await _redeSocialRepository.GetRedeSocialByIdAsync(id);

                if (redeSocial is null)
                    return NotFound();

                await _redeSocialRepository.Delete(redeSocial);

                return NoContent();                
            }
            catch (Exception)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}