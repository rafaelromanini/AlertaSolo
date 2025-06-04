using AlertaSolo.DTO;
using AlertaSolo.Model;
using AlertaSolo.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using AlertaSolo.Data;
using AlertaSolo.Data.Exceptions;

namespace AlertaSolo.Services
{
    public class LocalRiscoService : ILocalRiscoService
    {
        private readonly AppDbContext _context;

        public LocalRiscoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocalRiscoDTO>> GetAllAsync()
        {
            var locais = await _context.LocaisRisco
                .Include(l => l.Sensores)
                .ToListAsync();

            return locais.Select(l => new LocalRiscoDTO
            {
                Id = l.Id,
                NomeLocal = l.NomeLocal,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                Cidade = l.Cidade,
                Uf = l.Uf,
                GrauRisco = l.GrauRisco,
                Ativo = l.Ativo,
                Sensores = l.Sensores?.Select(s => new SensorDTO
                {
                    Id = s.Id,
                    CodigoEsp32 = s.CodigoEsp32,
                    Status = s.Status,
                    TipoSensor = s.TipoSensor,
                    DataInstalacao = s.DataInstalacao,
                    QntdAlertas = s.QntdAlertas,
                    LocalRiscoId = s.LocalRiscoId,
                    LocalRisco = null
                }).ToList()
            });
        }

        public async Task<LocalRiscoDTO> GetByIdAsync(int id)
        {
            var l = await _context.LocaisRisco
                .Include(l => l.Sensores)
                .FirstOrDefaultAsync(l => l.Id == id)
                ?? throw new LocalRiscoException("Local de risco não encontrado.");

            return new LocalRiscoDTO
            {
                Id = l.Id,
                NomeLocal = l.NomeLocal,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                Cidade = l.Cidade,
                Uf = l.Uf,
                GrauRisco = l.GrauRisco,
                Ativo = l.Ativo,
                Sensores = l.Sensores?.Select(s => new SensorDTO
                {
                    Id = s.Id,
                    CodigoEsp32 = s.CodigoEsp32,
                    Status = s.Status,
                    TipoSensor = s.TipoSensor,
                    DataInstalacao = s.DataInstalacao,
                    QntdAlertas = s.QntdAlertas,
                    LocalRiscoId = s.LocalRiscoId,
                    LocalRisco = null
                }).ToList()
            };
        }

        public async Task<LocalRiscoDTO> CreateAsync(LocalRiscoCreateDto dto)
        {
            var local = new LocalRisco
            {
                NomeLocal = dto.NomeLocal,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Cidade = dto.Cidade,
                Uf = dto.Uf,
                GrauRisco = dto.GrauRisco,
                Ativo = dto.Ativo,
            };

            _context.LocaisRisco.Add(local);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(local.Id);
        }

        public async Task<bool> UpdateAsync(int id, LocalRiscoUpdateDto dto)
        {
            var local = await _context.LocaisRisco.FindAsync(id)
                         ?? throw new LocalRiscoException("Local de risco não encontrado para atualização.");

            if (!string.IsNullOrEmpty(dto.NomeLocal))
                local.NomeLocal = dto.NomeLocal;

            if (!string.IsNullOrEmpty(dto.Latitude))
                local.Latitude = dto.Latitude;

            if (!string.IsNullOrEmpty(dto.Longitude))
                local.Longitude = dto.Longitude;

            if (!string.IsNullOrEmpty(dto.Cidade))
                local.Cidade = dto.Cidade;

            if (!string.IsNullOrEmpty(dto.Uf))
                local.Uf = dto.Uf;

            if (dto.GrauRisco.HasValue)
                local.GrauRisco = dto.GrauRisco.Value;

            if (dto.Ativo.HasValue)
                local.Ativo = dto.Ativo.Value;

            _context.LocaisRisco.Update(local);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var local = await _context.LocaisRisco.FindAsync(id)
                         ?? throw new LocalRiscoException("Local de risco não encontrado para exclusão.");

            _context.LocaisRisco.Remove(local);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
