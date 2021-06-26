using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.Api.Data;
using ProAgil.Api.Model;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    { 
        private readonly ILogger<EventoController> _logger;
        private readonly DataContext context;   

        public EventoController(ILogger<EventoController> logger, DataContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {            
            try
            {
                return Ok(context.Eventos.ToList());
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {   
            try
            {
                if (id.HasValue && id.Value > 0)
                {
                    return Ok(context.Eventos.FirstOrDefault(x => x.EventoId == id.Value));
                }
                throw new ArgumentException("Informe um id válido maior que 0.");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }        
    }
}
