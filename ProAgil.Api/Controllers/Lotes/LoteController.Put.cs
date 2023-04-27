using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.Lotes
{
    public partial class LoteController : ControllerBase
    {
        /// <summary>
        /// Atualiza as informações de um lote
        /// </summary>
        /// <param name="id">Id do lote a ser atualizado</param>
        /// <param name="loteDTO">Informações de atualização do lote</param>
        /// <returns><see cref="LoteDTO"/>Retorna o lote atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LoteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] LoteDTO loteDTO)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdAsync(id);

                if (lote is null)
                    return NotFound();

                _mapper.Map(loteDTO, lote);
                lote = await _loteRepository.Update(lote);
                _mapper.Map(lote, loteDTO);

                return Created($"api/evento/{loteDTO.Id}", loteDTO);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
