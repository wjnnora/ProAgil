using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ProAgil.Api.Controllers.Lotes
{
    public partial class LoteController : ControllerBase
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdAsync(id);

                if (lote is null)
                    return NotFound();

                await _loteRepository.Delete(lote);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
