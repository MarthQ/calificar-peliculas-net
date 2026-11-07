using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Blazor.Services;

public class ComentariosApiClient
{
    private readonly HttpClient http;
    private readonly SesionState sesion;

    public ComentariosApiClient(HttpClient http, SesionState sesion)
    {
        this.http = http;
        this.sesion = sesion;
    }

    private void Auth()
    {
        if (!string.IsNullOrEmpty(sesion.Token))
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", sesion.Token);
    }

    public async Task<List<ComentarioDTO>?> GetAllAsync() { Auth(); return await http.GetFromJsonAsync<List<ComentarioDTO>>("comentarios"); }
    public async Task<bool> AddAsync(ComentarioDTO dto) { Auth(); return (await http.PostAsJsonAsync("comentarios", dto)).IsSuccessStatusCode; }
    public async Task<bool> UpdateAsync(ComentarioDTO dto) { Auth(); return (await http.PutAsJsonAsync("comentarios", dto)).IsSuccessStatusCode; }
    public async Task<bool> DeleteAsync(int id) { Auth(); return (await http.DeleteAsync($"comentarios/{id}")).IsSuccessStatusCode; }
}
