using UsuarioEntidad = CalificarPeliculas.Domain.Users.Usuario;

namespace CalificarPeliculas.Application.Interfaces.Usuario
{
    public interface IPasswordHashService
    {
        string HashPassword(UsuarioEntidad usuario, string password);
        bool VerifyPassword(UsuarioEntidad usuario, string password);
    }
}
