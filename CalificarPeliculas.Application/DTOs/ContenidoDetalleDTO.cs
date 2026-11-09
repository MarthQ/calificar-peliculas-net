namespace CalificarPeliculas.Application.DTOs
{
    public class ContenidoDetalleDTO
    {
        public ContenidoDTO Contenido { get; set; } = null!;
        public List<CriticaDTO> Criticas { get; set; } = new();
    }
}
