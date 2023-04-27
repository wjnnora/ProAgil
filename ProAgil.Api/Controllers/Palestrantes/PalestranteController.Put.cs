using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.Palestrantes
{
    public partial class PalestranteController : ControllerBase
    {
        /// <summary>
        /// Atualiza um palestrante
        /// </summary>
        /// <param name="id">Id do palestrante</param>
        /// <param name="palestranteDTO">Novas informações do palestrante</param>
        /// <returns><see cref="PalestranteDTO"/>Retorna o palestrante com as novas informações cadastradas</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PalestranteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PalestranteDTO palestranteDTO)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);

                if (palestrante is null)
                    return NotFound();

                _mapper.Map(palestranteDTO, palestrante);
                palestrante = await _palestranteRepository.Update(palestrante);
                _mapper.Map(palestrante, palestranteDTO);

                return Created($"/api/palestrante/{palestranteDTO.Id}", palestranteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
