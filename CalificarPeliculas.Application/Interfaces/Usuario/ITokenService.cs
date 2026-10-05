using CalificarPeliculas.Application.DTOs;
using UsuarioEntity = CalificarPeliculas.Domain.Users.Usuario;

namespace CalificarPeliculas.Application.Interfaces.Usuario
{
    public interface ITokenService
    {
        string GenerateToken(UsuarioEntity usuario);
        DateTime GetExpiration();
    }
}
