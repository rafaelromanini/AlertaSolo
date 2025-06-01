using AlertaSolo.DTO;

namespace AlertaSolo.Services.Abstractions
{
    public interface ILocalRiscoService
    {
        Task<IEnumerable<LocalRiscoDTO>> GetAllAsync();
        Task<LocalRiscoDTO> GetByIdAsync(int id);
        Task<LocalRiscoDTO> CreateAsync(LocalRiscoCreateDto dto);
        Task<bool> UpdateAsync(int id, LocalRiscoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
