namespace CalificarPeliculas.Application.DTOs;

public class GrupoDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateOnly FechaCreacion { get; set; }
}
