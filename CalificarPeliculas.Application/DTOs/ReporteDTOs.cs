namespace CalificarPeliculas.Application.DTOs;

public class ReporteContenidoDTO
{
    public int ContenidoId { get; set; }
    public string Contenido { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public double PromedioCalificacion { get; set; }
    public int CantidadCriticas { get; set; }
    public int CantidadComentarios { get; set; }
}

public class ReporteDetalleDTO
{
    public string Contenido { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string TextoCritica { get; set; } = string.Empty;
    public float Calificacion { get; set; }
    public DateOnly FechaCritica { get; set; }
    public string TextoComentario { get; set; } = string.Empty;
    public string Reaccion { get; set; } = string.Empty;
    public DateOnly? FechaComentario { get; set; }
}
