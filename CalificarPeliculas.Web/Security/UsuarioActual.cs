using System.Security.Claims;
using CalificarPeliculas.Domain.Users;

namespace CalificarPeliculas.Web.Security
{
    public static class UsuarioActual
    {
        public static int Id(ClaimsPrincipal user) =>
            int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub"), out var id) ? id : 0;

        public static bool EsAdmin(ClaimsPrincipal user) => user.IsInRole(nameof(TipoRol.SuperUsuario));

        public static bool EsGestion(ClaimsPrincipal user) =>
            EsAdmin(user) || user.IsInRole(nameof(TipoRol.Moderador));
    }
}
