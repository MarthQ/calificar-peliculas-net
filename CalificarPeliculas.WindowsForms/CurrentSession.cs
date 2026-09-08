using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.WindowsForms
{
    // Sesión simple en memoria para la aplicación de escritorio.
    public static class CurrentSession
    {
        public static UsuarioDTO? User { get; set; }
    }
}
