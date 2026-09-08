using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioEntidad = CalificarPeliculas.Domain.Users.Usuario;

namespace CalificarPeliculas.Application.Interfaces.Usuario
{
    public interface IUsuarioRepository
    {
        Task AddAsync(UsuarioEntidad usuario);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(UsuarioEntidad usuario);
        Task<UsuarioEntidad?> GetByIdAsync(int id);
        Task<UsuarioEntidad?> GetByMailAsync(string mail);
        Task<UsuarioEntidad?> GetByNombreUsuarioAsync(string nombreUsuario);
        Task<IEnumerable<UsuarioEntidad>> GetAllAsync();
        Task<bool> ExistsByMailAsync(string mail);
        Task<bool> ExistsByNombreUsuarioAsync(string nombreUsuario);
        Task<bool> RolExistsAsync(int rolId);
    }
}
