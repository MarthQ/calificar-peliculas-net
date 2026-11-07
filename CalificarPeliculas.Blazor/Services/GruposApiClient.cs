using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Blazor.Services;

public class GruposApiClient
{
    private readonly HttpClient http;
    private readonly SesionState sesion;

    public GruposApiClient(HttpClient http, SesionState sesion)
    {
        this.http = http;
        this.sesion = sesion;
    }

    private void Auth()
    {
        if (!string.IsNullOrEmpty(sesion.Token))
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", sesion.Token);
    }

    public async Task<List<GrupoDTO>?> GetAllAsync() { Auth(); return await http.GetFromJsonAsync<List<GrupoDTO>>("grupos"); }
    public async Task<bool> AddAsync(GrupoDTO dto) { Auth(); return (await http.PostAsJsonAsync("grupos", dto)).IsSuccessStatusCode; }
    public async Task<bool> UpdateAsync(GrupoDTO dto) { Auth(); return (await http.PutAsJsonAsync("grupos", dto)).IsSuccessStatusCode; }
    public async Task<bool> DeleteAsync(int id) { Auth(); return (await http.DeleteAsync($"grupos/{id}")).IsSuccessStatusCode; }
}
