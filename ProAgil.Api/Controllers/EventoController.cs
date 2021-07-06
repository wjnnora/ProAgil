using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Repository;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    { 
        private readonly ILogger<EventoController> _logger;
        private readonly EventoContext context;   

        public EventoController(ILogger<EventoController> logger, EventoContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            try
            {                
                var resultado = await context.Eventos.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {   
            try
            {
                if (id.HasValue && id.Value > 0)
                {
                    var result = await context.Eventos.FirstOrDefaultAsync(x => x.Id == id.Value);                    
                    return Ok(result);
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
