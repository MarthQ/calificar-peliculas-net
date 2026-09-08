using CalificarPeliculas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Interfaces.Usuario
{
    public interface IUsuarioServicio
    {
        Task<UsuarioDTO> RegistrarAsync(RegistrarUsuarioDTO dto);
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<bool> UpdateAsync(ActualizarUsuarioDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<UsuarioDTO?> LoginAsync(LoginUsuarioDTO dto);
    }
}
