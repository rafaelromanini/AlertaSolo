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
            return await _context.LocaisRisco
                .Select(l => new LocalRiscoDTO
                {
                    Id = l.Id,
                    NomeLocal = l.NomeLocal,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Cidade = l.Cidade,
                    Uf = l.Uf,
                    GrauRisco = l.GrauRisco,
                    Ativo = l.Ativo,
                    UsuarioId = l.UsuarioId
                })
                .ToListAsync();
        }

        public async Task<LocalRiscoDTO> GetByIdAsync(int id)
        {
            var l = await _context.LocaisRisco.FindAsync(id)
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
                UsuarioId = l.UsuarioId
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
                UsuarioId = dto.UsuarioId
            };

            _context.LocaisRisco.Add(local);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(local.Id);
        }

        public async Task<bool> UpdateAsync(int id, LocalRiscoUpdateDto dto)
        {
            var local = await _context.LocaisRisco.FindAsync(id)
                         ?? throw new LocalRiscoException("Local de risco não encontrado para atualização.");

            local.NomeLocal = dto.NomeLocal;
            local.Latitude = dto.Latitude;
            local.Longitude = dto.Longitude;
            local.Cidade = dto.Cidade;
            local.Uf = dto.Uf;
            local.GrauRisco = dto.GrauRisco;
            local.Ativo = dto.Ativo;

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
