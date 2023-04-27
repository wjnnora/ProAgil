using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.Lotes
{
    public partial class LoteController : ControllerBase
    {
        [HttpPut("{id}")]
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
