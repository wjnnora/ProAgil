using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ProAgil.Api.Controllers.Palestrantes
{
    public partial class PalestranteController : ControllerBase
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);

                if (palestrante is null)
                    return NotFound();

                await _palestranteRepository.Delete(palestrante);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
