using System;
using System.IO;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using ProAgil.Domain;
using ProAgil.Api.DTO;
using ProAgil.Repository.Interfaces;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {        
        private IMapper _mapper;
        private IEventoRepository _eventoRepository;        

        public EventoController(IMapper mapper, IEventoRepository eventoRepository)
        {   
            _mapper = mapper;
            _eventoRepository = eventoRepository;            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync();
                var eventosDTO = _mapper.Map<IEnumerable<EventoDTO>>(eventos);

                return Ok(eventosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);
                var eventoDTO = _mapper.Map<EventoDTO>(evento);

                return Ok(eventoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }

        [HttpGet("getByTema/{tema}")]            
        public async Task<IActionResult> Get([FromRoute] string tema)
        {
            try
            {
                var evento = await _eventoRepository.GetEventosByTemaAsync(tema);
                var eventoDTO = _mapper.Map<EventoDTO>(evento);

                if (eventoDTO is not null)
                    return Ok(eventoDTO);

                return NotFound();
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
