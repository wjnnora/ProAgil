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
        /// <summary>
        /// Atualiza um evento
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <param name="eventoDTO">Informações da atualização do evento</param>
        /// <returns><see cref="EventoDTO"/>Evento que foi inserido no banco</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EventoDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EventoDTO eventoDTO)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);

                if (evento is null)
                    return NotFound($"Evento com o Id {id} não existe.");

                var idsLotes = eventoDTO.Lotes.Select(x => x.Id).ToList();
                var idsRedesSociais = eventoDTO.RedesSociais.Select(x => x.Id).ToList();

                var lotes = evento.Lotes.Where(lote => !idsLotes.Contains(lote.Id));
                var redesSociais = evento.RedesSociais.Where(redeSocial => !idsRedesSociais.Contains(redeSocial.Id));                

                _mapper.Map(eventoDTO, evento);
                evento = await _eventoRepository.Update(evento);
                _mapper.Map(evento, eventoDTO);

                return Created($"/api/evento/{eventoDTO.Id}", eventoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
