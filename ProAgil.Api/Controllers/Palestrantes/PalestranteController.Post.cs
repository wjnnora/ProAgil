using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Domain;
using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.Palestrantes
{
    public partial class PalestranteController : ControllerBase 
    {
        /// <summary>
        /// Cria um novo palestrante
        /// </summary>
        /// <param name="palestranteDTO">Informações do novo palestrante</param>
        /// <returns><see cref="PalestranteDTO"/>Retorna o novo palestrante cadastrado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PalestranteDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Post([FromBody] PalestranteDTO palestranteDTO)
        {
            try
            {
                var palestrante = _mapper.Map<Palestrante>(palestranteDTO);
                palestrante = await _palestranteRepository.Insert(palestrante);
                _mapper.Map(palestrante, palestranteDTO);

                return Created($"api/palestrante/{palestranteDTO.Id}", palestranteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
