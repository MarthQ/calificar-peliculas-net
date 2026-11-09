using System.Net.Http.Headers;
using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Application.Interfaces.Usuario;

namespace CalificarPeliculas.WindowsForms;

internal sealed class WinFormsTokenHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(CurrentSession.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CurrentSession.Token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}

internal sealed class ApiUsuarioServicio : IUsuarioServicio
{
    private readonly HttpClient http;

    public ApiUsuarioServicio(HttpClient http)
    {
        this.http = http;
    }

    public async Task<UsuarioDTO?> LoginAsync(LoginUsuarioDTO dto)
    {
        var response = await http.PostAsJsonAsync("auth/login", dto);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var login = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        if (login != null)
        {
            CurrentSession.Start(login);
        }

        return login?.Usuario;
    }

    public async Task<LoginResponseDTO?> LoginConTokenAsync(LoginUsuarioDTO dto)
    {
        var response = await http.PostAsJsonAsync("auth/login", dto);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var login = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        if (login != null)
        {
            CurrentSession.Start(login);
        }

        return login;
    }

    public async Task<UsuarioDTO> RegistrarAsync(RegistrarUsuarioDTO dto)
    {
        var response = await http.PostAsJsonAsync("usuarios", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<UsuarioDTO>())!;
    }

    public async Task<UsuarioDTO?> GetByIdAsync(int id) =>
        await http.GetFromJsonAsync<UsuarioDTO>($"usuarios/{id}");

    public async Task<IEnumerable<UsuarioDTO>> GetAllAsync() =>
        (await http.GetFromJsonAsync<List<UsuarioDTO>>("usuarios")) ?? new List<UsuarioDTO>();

    public async Task<bool> UpdateAsync(ActualizarUsuarioDTO dto) =>
        (await http.PutAsJsonAsync("usuarios", dto)).IsSuccessStatusCode;

    public async Task<bool> DeleteAsync(int id) =>
        (await http.DeleteAsync($"usuarios/{id}")).IsSuccessStatusCode;
}

internal sealed class ApiGeneroServicio : IGeneroServicio
{
    private readonly HttpClient http;

    public ApiGeneroServicio(HttpClient http)
    {
        this.http = http;
    }

    public async Task<GeneroDTO> AddAsync(GeneroDTO dto)
    {
        var response = await http.PostAsJsonAsync("generos", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GeneroDTO>())!;
    }

    public async Task<bool> DeleteAsync(int id) =>
        (await http.DeleteAsync($"generos/{id}")).IsSuccessStatusCode;

    public async Task<GeneroDTO?> GetAsync(int id) =>
        await http.GetFromJsonAsync<GeneroDTO>($"generos/{id}");

    public async Task<IEnumerable<GeneroDTO>> GetAllAsync() =>
        (await http.GetFromJsonAsync<List<GeneroDTO>>("generos")) ?? new List<GeneroDTO>();

    public async Task<bool> UpdateAsync(GeneroDTO dto) =>
        (await http.PutAsJsonAsync("generos", dto)).IsSuccessStatusCode;
}

internal sealed class ApiContenidoServicio : IContenidoServicio
{
    private readonly HttpClient http;

    public ApiContenidoServicio(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ContenidoDTO> AddAsync(ContenidoDTO dto)
    {
        var response = await http.PostAsJsonAsync("contenidos", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<ContenidoDTO>())!;
    }

    public async Task<bool> DeleteAsync(int id) =>
        (await http.DeleteAsync($"contenidos/{id}")).IsSuccessStatusCode;

    public async Task<ContenidoDTO?> GetAsync(int id) =>
        await http.GetFromJsonAsync<ContenidoDTO>($"contenidos/{id}");

    public async Task<ContenidoDetalleDTO?> GetWithCriticasAsync(int id) =>
        await http.GetFromJsonAsync<ContenidoDetalleDTO>($"contenidos/{id}/criticas");

    public async Task<IEnumerable<ContenidoDTO>> GetAllAsync() =>
        (await http.GetFromJsonAsync<List<ContenidoDTO>>("contenidos")) ?? new List<ContenidoDTO>();

    public async Task<bool> UpdateAsync(ContenidoDTO dto) =>
        (await http.PutAsJsonAsync("contenidos", dto)).IsSuccessStatusCode;
}

internal sealed class ApiCriticaServicio : ICriticaServicio
{
    private readonly HttpClient http;

    public ApiCriticaServicio(HttpClient http)
    {
        this.http = http;
    }

    public async Task<CriticaDTO> AddAsync(CriticaDTO dto)
    {
        var response = await http.PostAsJsonAsync("criticas", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CriticaDTO>())!;
    }

    public async Task<bool> DeleteAsync(int id) =>
        (await http.DeleteAsync($"criticas/{id}")).IsSuccessStatusCode;

    public async Task<CriticaDTO?> GetAsync(int id) =>
        await http.GetFromJsonAsync<CriticaDTO>($"criticas/{id}");

    public async Task<IEnumerable<CriticaDTO>> GetAllAsync() =>
        (await http.GetFromJsonAsync<List<CriticaDTO>>("criticas")) ?? new List<CriticaDTO>();

    public async Task<bool> UpdateAsync(CriticaDTO dto) =>
        (await http.PutAsJsonAsync("criticas", dto)).IsSuccessStatusCode;
}
