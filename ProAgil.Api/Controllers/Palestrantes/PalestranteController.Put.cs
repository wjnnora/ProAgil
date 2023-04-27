using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ProAgil.Api.DTO;

namespace ProAgil.Api.Controllers.Palestrantes
{
    public partial class PalestranteController : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PalestranteDTO palestranteDTO)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(id);

                if (palestrante is null)
                    return NotFound();

                _mapper.Map(palestranteDTO, palestrante);
                palestrante = await _palestranteRepository.Update(palestrante);
                _mapper.Map(palestrante, palestranteDTO);

                return Created($"/api/palestrante/{palestranteDTO.Id}", palestranteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor.");
            }
        }
    }
}
