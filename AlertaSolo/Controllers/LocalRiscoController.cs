using AlertaSolo.DTO;
using AlertaSolo.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AlertaSolo.Data.Exceptions;

namespace AlertaSolo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalRiscoController : ControllerBase
    {
        private readonly ILocalRiscoService _service;

        public LocalRiscoController(ILocalRiscoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var local = await _service.GetByIdAsync(id);
                return Ok(local);
            }
            catch (LocalRiscoException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocalRiscoCreateDto dto)
        {
            var local = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = local.Id }, local);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LocalRiscoUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (LocalRiscoException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (LocalRiscoException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }
    }
}
