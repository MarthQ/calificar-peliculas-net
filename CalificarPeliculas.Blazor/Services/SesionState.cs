using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Blazor.Services
{
    // Estado de sesion en memoria para el usuario autenticado en Blazor Server.
    public class SesionState
    {
        public string? Token { get; private set; }
        public UsuarioDTO? Usuario { get; private set; }
        public bool EstaAutenticado => !string.IsNullOrEmpty(Token) && Usuario != null;
        public bool EsAdmin => Usuario?.RolId == 1;

        public event Action? OnChange;

        public void IniciarSesion(string token, UsuarioDTO usuario)
        {
            Token = token;
            Usuario = usuario;
            OnChange?.Invoke();
        }

        public void CerrarSesion()
        {
            Token = null;
            Usuario = null;
            OnChange?.Invoke();
        }
    }
}
