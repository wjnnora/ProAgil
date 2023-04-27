using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Domain;
using ProAgil.Api.DTO;
using System.Net.Http.Headers;

namespace ProAgil.Api.Controllers.Eventos
{
    public partial class EventoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventoDTO eventoDTO)
        {
            try
            {
                var evento = _mapper.Map<Evento>(eventoDTO);
                evento = await _eventoRepository.Insert(evento);
                _mapper.Map(evento, eventoDTO);

                return Created($"/api/evento/{eventoDTO.Id}", eventoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpPost("upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = Path.Combine("Resources", "Images");
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    string fileNameToSave = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    fileNameToSave = fileNameToSave.Replace("\"", "").Trim();
                    string fullPathToSave = Path.Combine(fullPath, fileNameToSave);

                    using (var stream = new FileStream(fullPathToSave, FileMode.Create))
                        file.CopyTo(stream);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
