using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.WindowsForms
{
    // Sesión simple en memoria para la aplicación de escritorio.
    public static class CurrentSession
    {
        public static UsuarioDTO? User { get; set; }
        public static string? Token { get; private set; }
        public static bool IsAdmin => User?.RolId == 1;
        public static bool EsGestion => User?.RolId is 1 or 2;

        public static void Start(LoginResponseDTO login)
        {
            Token = login.Token;
            User = login.Usuario;
        }

        public static void Clear()
        {
            Token = null;
            User = null;
        }
    }
}
