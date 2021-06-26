using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<Evento> Get()
        {            
            return context.Eventos.ToList();
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {                        
            return context.Eventos.Where(x => x.EventoId == id).ToList();
        }        
    }
}
