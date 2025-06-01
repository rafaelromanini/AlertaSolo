using AlertaSolo.DTO;
using AlertaSolo.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AlertaSolo.Data.Exceptions;

namespace AlertaSolo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
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
                var usuario = await _service.GetByIdAsync(id);
                return Ok(usuario);
            }
            catch (UsuarioException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDto dto)
        {
            var usuario = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (UsuarioException ex)
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
            catch (UsuarioException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }
    }
}
