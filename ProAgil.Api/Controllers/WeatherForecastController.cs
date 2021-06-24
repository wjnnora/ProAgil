using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.Api.Model;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {                        
            return GetEventos();
        }        

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {
            return GetEventos().Where(x => x.EventoId == id).ToList();
        }        

        private IEnumerable<Evento> GetEventos()
        {
            return Enumerable.Range(1, 10).Select(index => new Evento
            {
                EventoId = index,
                Local = "São João da Boa Vista",
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy HH:MM"),
                Tema = "Foda-se",
                QtdPessoas = 25,
                Lote = "Primeiro Lote"
            })
            .ToArray();
        }
    }
}
