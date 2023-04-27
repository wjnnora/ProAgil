using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ProAgil.Api.Controllers.RedesSociais
{
    public partial class RedeSocialController : ControllerBase
    {
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
