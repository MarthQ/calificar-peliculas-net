using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class GenerosApiClient
    {
        private readonly HttpClient http;
        private readonly SesionState sesion;

        public GenerosApiClient(HttpClient http, SesionState sesion)
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

        public Task<List<GeneroDTO>?> GetAllAsync() =>
            http.GetFromJsonAsync<List<GeneroDTO>>("generos");

        public async Task<bool> AddAsync(GeneroDTO dto)
        {
            SetAuthorizationHeader();
            var response = await http.PostAsJsonAsync("generos", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(GeneroDTO dto)
        {
            SetAuthorizationHeader();
            var response = await http.PutAsJsonAsync("generos", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            SetAuthorizationHeader();
            var response = await http.DeleteAsync($"generos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
