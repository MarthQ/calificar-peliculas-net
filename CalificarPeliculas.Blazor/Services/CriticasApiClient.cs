using CalificarPeliculas.Application.DTOs;
using System.Net.Http.Json;

namespace CalificarPeliculas.Blazor.Services
{
    public class CriticasApiClient
    {
        private readonly HttpClient http;
        private readonly SesionState sesion;
        public CriticasApiClient(HttpClient http, SesionState sesion)
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

        public Task<List<CriticaDTO>?> GetAllAsync() =>
            http.GetFromJsonAsync<List<CriticaDTO>>("criticas");

        public async Task<bool> AddAsync(CriticaDTO dto)
        {
            SetAuthorizationHeader();
            var response = await http.PostAsJsonAsync("criticas", dto);
            return response.IsSuccessStatusCode;
        }
            

        public async Task<bool> UpdateAsync(CriticaDTO dto) 
        {
            SetAuthorizationHeader();
            var response = await http.PutAsJsonAsync("criticas", dto);
            return response.IsSuccessStatusCode;
        }
            

        public async Task<bool> DeleteAsync(int id) 
        {
            SetAuthorizationHeader();
            var response = await http.DeleteAsync($"criticas/{id}");
            return response.IsSuccessStatusCode;
        }
            
    }
}
