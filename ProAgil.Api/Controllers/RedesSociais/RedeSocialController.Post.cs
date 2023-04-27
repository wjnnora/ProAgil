using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Domain;
using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.RedesSociais
{
    public partial class RedeSocialController : ControllerBase
    {
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
    }
}
