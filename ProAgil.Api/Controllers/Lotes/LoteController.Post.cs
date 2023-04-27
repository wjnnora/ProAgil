using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;
using ProAgil.Domain;

namespace ProAgil.Api.Controllers.Lotes
{
    public partial class LoteController : ControllerBase
    {
        /// <summary>
        /// Cria um novo lote
        /// </summary>
        /// <param name="loteDTO">Informações do novo lote</param>
        /// <returns><see cref="LoteDTO"/>Lote cadastrado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Post([FromBody] LoteDTO loteDTO)
        {
            try
            {
                var lote = _mapper.Map<Lote>(loteDTO);
                lote = await _loteRepository.Insert(lote);
                _mapper.Map(lote, loteDTO);

                return Created($"api/lote/{loteDTO.Id}", loteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
