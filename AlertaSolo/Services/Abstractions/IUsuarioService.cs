using AlertaSolo.DTO;

namespace AlertaSolo.Services.Abstractions
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO> GetByIdAsync(int id);
        Task<UsuarioDTO> CreateAsync(UsuarioCreateDto dto);
        Task<bool> UpdateAsync(int id, UsuarioUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
