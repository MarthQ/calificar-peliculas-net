using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class GenerosApiClient
    {
        private readonly HttpClient http;

        public GenerosApiClient(HttpClient http)
        {
            this.http = http;
        }

        public Task<List<GeneroDTO>?> GetAllAsync() =>
            http.GetFromJsonAsync<List<GeneroDTO>>("generos");

        public async Task<bool> AddAsync(GeneroDTO dto) =>
            (await http.PostAsJsonAsync("generos", dto)).IsSuccessStatusCode;

        public async Task<bool> UpdateAsync(GeneroDTO dto) =>
            (await http.PutAsJsonAsync("generos", dto)).IsSuccessStatusCode;

        public async Task<bool> DeleteAsync(int id) =>
            (await http.DeleteAsync($"generos/{id}")).IsSuccessStatusCode;
    }
}
