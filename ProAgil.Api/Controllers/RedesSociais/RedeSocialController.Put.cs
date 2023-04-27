using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.RedesSociais
{
    public partial class RedeSocialController : ControllerBase
    {
        /// <summary>
        /// Atualiza uma Rede Social
        /// </summary>
        /// <param name="id">Id da Rede Social</param>
        /// <param name="redeSocialDTO">Novas informações da Rede Social</param>
        /// <returns><see cref="RedeSocialDTO"/>Retorna a Rede Social cadastrada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RedeSocialDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
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
    }
}
