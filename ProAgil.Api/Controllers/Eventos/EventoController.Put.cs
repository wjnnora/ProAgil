using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.Eventos
{
    public partial class EventoController : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EventoDTO eventoDTO)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);

                var idsLotes = eventoDTO.Lotes.Select(x => x.Id).ToList();
                var idsRedesSociais = eventoDTO.RedesSociais.Select(x => x.Id).ToList();

                var lotes = evento.Lotes.Where(lote => !idsLotes.Contains(lote.Id));
                var redesSociais = evento.RedesSociais.Where(redeSocial => !idsRedesSociais.Contains(redeSocial.Id));

                if (evento != null)
                {
                    _mapper.Map(eventoDTO, evento);
                    evento = await _eventoRepository.Update(evento);
                    _mapper.Map(evento, eventoDTO);

                    return Created($"/api/evento/{eventoDTO.Id}", eventoDTO);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
