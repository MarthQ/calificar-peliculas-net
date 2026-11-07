namespace CalificarPeliculas.Application.DTOs;

public class ComentarioDTO
{
    public int Id { get; set; }
    public int CriticaId { get; set; }
    public int UsuarioId { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateOnly FechaRealizacion { get; set; }
    public string Reaccion { get; set; } = string.Empty;
}
