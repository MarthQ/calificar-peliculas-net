using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.WindowsForms;

public sealed class ReporteApiServicio
{
    private readonly HttpClient http;

    public ReporteApiServicio(HttpClient http)
    {
        this.http = http;
    }

    public async Task<List<ReporteContenidoDTO>> GetResumenAsync() =>
        (await http.GetFromJsonAsync<List<ReporteContenidoDTO>>("reportes/contenidos/resumen")) ?? new();

    public async Task<List<ReporteDetalleDTO>> GetDetalleAsync() =>
        (await http.GetFromJsonAsync<List<ReporteDetalleDTO>>("reportes/contenidos/detalle")) ?? new();
}
