using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class ContenidosApiClient
    {
        private readonly HttpClient http;

        public ContenidosApiClient(HttpClient http)
        {
            this.http = http;
        }

        public Task<List<ContenidoDTO>?> GetAllAsync() =>
            http.GetFromJsonAsync<List<ContenidoDTO>>("contenidos");

        public async Task<string?> AddAsync(ContenidoDTO dto)
        {
            var response = await http.PostAsJsonAsync("contenidos", dto);
            return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
        }

        public async Task<string?> UpdateAsync(ContenidoDTO dto)
        {
            var response = await http.PutAsJsonAsync("contenidos", dto);
            return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> DeleteAsync(int id) =>
            (await http.DeleteAsync($"contenidos/{id}")).IsSuccessStatusCode;
    }
}
