using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CalificarPeliculas.Repository.Users
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private static CFContext CreateContext()
        {
            return new CFContext();
        }

        public async Task AddAsync(Usuario usuario)
        {
            using var context = CreateContext();
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
                return false;

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            using var context = CreateContext();

            if (!await context.Usuarios.AnyAsync(u => u.Id == usuario.Id))
                return false;

            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            return await context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> GetByMailAsync(string mail)
        {
            using var context = CreateContext();
            var mailNormalizado = mail.Trim();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Mail == mailNormalizado);
        }

        public async Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario)
        {
            using var context = CreateContext();
            var nombreNormalizado = nombreUsuario.Trim();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreNormalizado);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Usuarios.ToListAsync();
        }

        public async Task<bool> ExistsByMailAsync(string mail)
        {
            using var context = CreateContext();
            var mailNormalizado = mail.Trim();
            return await context.Usuarios.AnyAsync(u => u.Mail == mailNormalizado);
        }

        public async Task<bool> ExistsByNombreUsuarioAsync(string nombreUsuario)
        {
            using var context = CreateContext();
            var nombreNormalizado = nombreUsuario.Trim();
            return await context.Usuarios.AnyAsync(u => u.NombreUsuario == nombreNormalizado);
        }

        public async Task<bool> RolExistsAsync(int rolId)
        {
            using var context = CreateContext();
            return await context.Roles.AnyAsync(r => r.Id == rolId);
        }
    }
}
