using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Blazor.Services;

public class UsuariosApiClient
{
    private readonly HttpClient http;
    private readonly SesionState sesion;

    public UsuariosApiClient(HttpClient http, SesionState sesion)
    {
        this.http = http;
        this.sesion = sesion;
    }

    private void Auth()
    {
        if (!string.IsNullOrEmpty(sesion.Token))
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", sesion.Token);
    }

    public async Task<List<UsuarioDTO>?> GetAllAsync() { Auth(); return await http.GetFromJsonAsync<List<UsuarioDTO>>("usuarios"); }
    public async Task<bool> AddAsync(RegistrarUsuarioDTO dto) { Auth(); return (await http.PostAsJsonAsync("usuarios", dto)).IsSuccessStatusCode; }
    public async Task<bool> UpdateAsync(ActualizarUsuarioDTO dto) { Auth(); return (await http.PutAsJsonAsync("usuarios", dto)).IsSuccessStatusCode; }
    public async Task<bool> DeleteAsync(int id) { Auth(); return (await http.DeleteAsync($"usuarios/{id}")).IsSuccessStatusCode; }
}
