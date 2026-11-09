using System.Net.Http.Json;
using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Blazor.Services;

public class ReportesApiClient
{
    private readonly HttpClient http;
    private readonly SesionState sesion;

    public ReportesApiClient(HttpClient http, SesionState sesion)
    {
        this.http = http;
        this.sesion = sesion;
    }

    private void Auth()
    {
        if (!string.IsNullOrEmpty(sesion.Token))
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", sesion.Token);
    }

    public async Task<List<ReporteContenidoDTO>?> GetResumenAsync()
    {
        Auth();
        return await http.GetFromJsonAsync<List<ReporteContenidoDTO>>("reportes/contenidos/resumen");
    }

    public async Task<List<ReporteDetalleDTO>?> GetDetalleAsync()
    {
        Auth();
        return await http.GetFromJsonAsync<List<ReporteDetalleDTO>>("reportes/contenidos/detalle");
    }
}
