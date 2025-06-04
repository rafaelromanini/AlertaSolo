using AlertaSolo.DTO;
using AlertaSolo.Model;
using AlertaSolo.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using AlertaSolo.Data;
using AlertaSolo.Data.Exceptions;

namespace AlertaSolo.Services
{
    public class SensorService : ISensorService
    {
        private readonly AppDbContext _context;

        public SensorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SensorDTO>> GetAllAsync()
        {
            return await _context.Sensores
                .Include(s => s.LocalRisco)
                .Select(s => new SensorDTO
                {
                    Id = s.Id,
                    CodigoEsp32 = s.CodigoEsp32,
                    Status = s.Status,
                    TipoSensor = s.TipoSensor,
                    DataInstalacao = s.DataInstalacao,
                    QntdAlertas = s.QntdAlertas,
                    LocalRiscoId = s.LocalRiscoId,
                    LocalRisco = new LocalRiscoDTO
                    {
                        Id = s.LocalRisco.Id,
                        NomeLocal = s.LocalRisco.NomeLocal,
                        Latitude = s.LocalRisco.Latitude,
                        Longitude = s.LocalRisco.Longitude,
                        Cidade = s.LocalRisco.Cidade,
                        Uf = s.LocalRisco.Uf,
                        GrauRisco = s.LocalRisco.GrauRisco,
                        Ativo = s.LocalRisco.Ativo,
                        Sensores = null
                    }
                })
                .ToListAsync();
        }

        public async Task<SensorDTO> GetByIdAsync(int id)
        {
            var s = await _context.Sensores
                .Include(s => s.LocalRisco)
                .FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new SensorException("Sensor não encontrado.");

            return new SensorDTO
            {
                Id = s.Id,
                CodigoEsp32 = s.CodigoEsp32,
                Status = s.Status,
                TipoSensor = s.TipoSensor,
                DataInstalacao = s.DataInstalacao,
                QntdAlertas = s.QntdAlertas,
                LocalRiscoId = s.LocalRiscoId,
                LocalRisco = new LocalRiscoDTO
                {
                    Id = s.LocalRisco.Id,
                    NomeLocal = s.LocalRisco.NomeLocal,
                    Latitude = s.LocalRisco.Latitude,
                    Longitude = s.LocalRisco.Longitude,
                    Cidade = s.LocalRisco.Cidade,
                    Uf = s.LocalRisco.Uf,
                    GrauRisco = s.LocalRisco.GrauRisco,
                    Ativo = s.LocalRisco.Ativo,
                    Sensores = null
                }
            };
        }

        public async Task<SensorDTO> CreateAsync(SensorCreateDto dto)
        {
            var sensor = new Sensor
            {
                CodigoEsp32 = dto.CodigoEsp32,
                Status = dto.Status,
                TipoSensor = dto.TipoSensor,
                QntdAlertas = dto.QntdAlertas,
                LocalRiscoId = dto.LocalRiscoId
            };

            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(sensor.Id);
        }

        public async Task<bool> UpdateAsync(int id, SensorUpdateDto dto)
        {
            var sensor = await _context.Sensores.FindAsync(id)
                          ?? throw new SensorException("Sensor não encontrado para atualização.");

            if (!string.IsNullOrEmpty(dto.Status))
                sensor.Status = dto.Status;

            if (!string.IsNullOrEmpty(dto.TipoSensor))
                sensor.TipoSensor = dto.TipoSensor;

            if (dto.QntdAlertas.HasValue)
                sensor.QntdAlertas = dto.QntdAlertas.Value;

            _context.Sensores.Update(sensor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sensor = await _context.Sensores.FindAsync(id)
                          ?? throw new SensorException("Sensor não encontrado para exclusão.");

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
