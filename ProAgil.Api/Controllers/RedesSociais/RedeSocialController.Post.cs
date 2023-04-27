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
        /// <summary>
        /// Cria uma nova Rede Social
        /// </summary>
        /// <param name="redeSocialDTO">Informações da nova Rede Social</param>
        /// <returns><see cref="RedeSocialDTO"/>Retorna a nova Rede Social cadastrada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RedeSocialDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
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
