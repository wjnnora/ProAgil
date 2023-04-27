using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ProAgil.Api.Controllers.Eventos
{
    public partial class EventoController : ControllerBase
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);

                if (evento is null)
                    return NotFound();

                await _eventoRepository.Delete(evento);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
