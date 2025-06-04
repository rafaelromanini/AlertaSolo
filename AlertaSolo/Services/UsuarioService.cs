using AlertaSolo.DTO;
using AlertaSolo.Model;
using AlertaSolo.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using AlertaSolo.Data;
using AlertaSolo.Data.Exceptions;

namespace AlertaSolo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Cpf = u.Cpf,
                    Idade = u.Idade,
                    Cidade = u.Cidade,
                    Uf = u.Uf,
                    DataCadastro = u.DataCadastro,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var u = await _context.Usuarios.FindAsync(id)
                     ?? throw new UsuarioException("Usuário não encontrado.");

            return new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Cpf = u.Cpf,
                Idade = u.Idade,
                Cidade = u.Cidade,
                Uf = u.Uf,
                DataCadastro = u.DataCadastro,
                Email = u.Email
            };
        }

        public async Task<UsuarioDTO> CreateAsync(UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                Idade = dto.Idade,
                Cidade = dto.Cidade,
                Uf = dto.Uf,
                Email = dto.Email,
                Senha = dto.Senha
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(usuario.Id);
        }

        public async Task<bool> UpdateAsync(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                          ?? throw new UsuarioException("Usuário não encontrado para atualização.");

            if (!string.IsNullOrEmpty(dto.Nome))
                usuario.Nome = dto.Nome;

            if (dto.Idade.HasValue)
                usuario.Idade = dto.Idade.Value;

            if (!string.IsNullOrEmpty(dto.Cidade))
                usuario.Cidade = dto.Cidade;

            if (!string.IsNullOrEmpty(dto.Uf))
                usuario.Uf = dto.Uf;

            if (!string.IsNullOrEmpty(dto.Email))
                usuario.Email = dto.Email;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                          ?? throw new UsuarioException("Usuário não encontrado para exclusão.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
