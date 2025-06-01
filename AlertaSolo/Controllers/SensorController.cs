using AlertaSolo.DTO;
using AlertaSolo.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AlertaSolo.Data.Exceptions;

namespace AlertaSolo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _service;

        public SensorController(ISensorService service)
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
                var sensor = await _service.GetByIdAsync(id);
                return Ok(sensor);
            }
            catch (SensorException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SensorCreateDto dto)
        {
            var sensor = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, sensor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SensorUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (SensorException ex)
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
            catch (SensorException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }
    }
}
