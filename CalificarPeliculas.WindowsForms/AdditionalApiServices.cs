using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Comentarios;
using CalificarPeliculas.Application.Interfaces.Grupo;
using CalificarPeliculas.Domain;
using GrupoEntidad = CalificarPeliculas.Domain.Users.Grupo;

namespace CalificarPeliculas.WindowsForms;

internal sealed class ApiComentarioServicio : IComentarioServicio
{
    private readonly HttpClient http;
    public ApiComentarioServicio(HttpClient http) => this.http = http;
    public async Task<ComentarioDTO> AddAsync(ComentarioDTO dto) { var r = await http.PostAsJsonAsync("comentarios", dto); r.EnsureSuccessStatusCode(); return (await r.Content.ReadFromJsonAsync<ComentarioDTO>())!; }
    public async Task<bool> DeleteAsync(int id) => (await http.DeleteAsync($"comentarios/{id}")).IsSuccessStatusCode;
    public async Task<ComentarioDTO?> GetAsync(int id) => await http.GetFromJsonAsync<ComentarioDTO>($"comentarios/{id}");
    public async Task<IEnumerable<ComentarioDTO>> GetAllAsync() => (await http.GetFromJsonAsync<List<ComentarioDTO>>("comentarios")) ?? new();
    public async Task<bool> UpdateAsync(ComentarioDTO dto) => (await http.PutAsJsonAsync("comentarios", dto)).IsSuccessStatusCode;
}

internal sealed class ApiGrupoServicio : IGrupoServicio
{
    private readonly HttpClient http;
    public ApiGrupoServicio(HttpClient http) => this.http = http;
    public async Task<GrupoDTO> AddAsync(GrupoDTO dto) { var r = await http.PostAsJsonAsync("grupos", dto); r.EnsureSuccessStatusCode(); return (await r.Content.ReadFromJsonAsync<GrupoDTO>())!; }
    public async Task<bool> DeleteAsync(int id) => (await http.DeleteAsync($"grupos/{id}")).IsSuccessStatusCode;
    public async Task<GrupoDTO?> GetAsync(int id) => await http.GetFromJsonAsync<GrupoDTO>($"grupos/{id}");
    public async Task<IEnumerable<GrupoDTO>> GetAllAsync() => (await http.GetFromJsonAsync<List<GrupoDTO>>("grupos")) ?? new();
    public async Task<bool> UpdateAsync(GrupoDTO dto) => (await http.PutAsJsonAsync("grupos", dto)).IsSuccessStatusCode;
}
