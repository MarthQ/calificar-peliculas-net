using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class CriticasApiClient
    {
        private readonly HttpClient http;

        public CriticasApiClient(HttpClient http)
        {
            this.http = http;
        }

        public Task<List<CriticaDTO>?> GetAllAsync() =>
            http.GetFromJsonAsync<List<CriticaDTO>>("criticas");

        public async Task<bool> AddAsync(CriticaDTO dto) =>
            (await http.PostAsJsonAsync("criticas", dto)).IsSuccessStatusCode;

        public async Task<bool> UpdateAsync(CriticaDTO dto) =>
            (await http.PutAsJsonAsync("criticas", dto)).IsSuccessStatusCode;

        public async Task<bool> DeleteAsync(int id) =>
            (await http.DeleteAsync($"criticas/{id}")).IsSuccessStatusCode;
    }
}
