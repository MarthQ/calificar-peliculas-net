using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class AuthApiClient
    {
        private readonly HttpClient http;
        private readonly SesionState sesion;

        public AuthApiClient(HttpClient http, SesionState sesion)
        {
            this.http = http;
            this.sesion = sesion;
        }

        public async Task<bool> LoginAsync(string mail, string password)
        {
            var response = await http.PostAsJsonAsync("auth/login", new LoginUsuarioDTO
            {
                Mail = mail,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return false;

            var login = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            if (login == null)
                return false;

            sesion.IniciarSesion(login.Token, login.Usuario);
            return true;
        }

        public void Logout() => sesion.CerrarSesion();
    }
}
