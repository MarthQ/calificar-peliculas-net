using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class ContenidosApiClient
    {
        private readonly HttpClient http;
        private readonly SesionState sesion;

        public ContenidosApiClient(HttpClient http, SesionState sesion)
        {
            this.http = http;
            this.sesion = sesion;
        }
        private void SetAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(sesion.Token))
            {
                http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", sesion.Token);
            }
        }
        public Task<List<ContenidoDTO>?> GetAllAsync() =>
            http.GetFromJsonAsync<List<ContenidoDTO>>("contenidos");

        public async Task<string?> AddAsync(ContenidoDTO dto)
        {
            SetAuthorizationHeader();
            var response = await http.PostAsJsonAsync("contenidos", dto);
            return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
        }

        public async Task<string?> UpdateAsync(ContenidoDTO dto)
        {
            SetAuthorizationHeader();
            var response = await http.PutAsJsonAsync("contenidos", dto);
            return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> DeleteAsync(int id) 
        { 
            SetAuthorizationHeader();
            var response = await http.DeleteAsync($"contenidos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
