using AlertaSolo.DTO;

namespace AlertaSolo.Services.Abstractions
{
    public interface ISensorService
    {
        Task<IEnumerable<SensorDTO>> GetAllAsync();
        Task<SensorDTO> GetByIdAsync(int id);
        Task<SensorDTO> CreateAsync(SensorCreateDto dto);
        Task<bool> UpdateAsync(int id, SensorUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
