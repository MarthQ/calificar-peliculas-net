using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CalificarPeliculas.Web.Security
{
    public class AspNetPasswordHashService : IPasswordHashService
    {
        private readonly IPasswordHasher<Usuario> passwordHasher;

        public AspNetPasswordHashService(IPasswordHasher<Usuario> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public string HashPassword(Usuario usuario, string password)
        {
            return passwordHasher.HashPassword(usuario, password);
        }

        public bool VerifyPassword(Usuario usuario, string password)
        {
            var resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.PasswordHash,
                password);

            return resultado != PasswordVerificationResult.Failed;
        }
    }
}
