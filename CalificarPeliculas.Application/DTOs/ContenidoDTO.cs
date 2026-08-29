using CalificarPeliculas.Domain;
using System.Text.Json.Serialization;


namespace CalificarPeliculas.Application.DTOs
{
    public class ContenidoDTO
    {
        public int Id { get; set; }
        public int IdTMDB { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreDirector { get; set; }
        public DateOnly FechaLanzamiento { get; set; }
        public TimeOnly Duracion { get; set; }
        public string UrlImagen { get; set; }
        public float PuntuacionPromedio { get; set; }
        public int GeneroId { get; set; }
        public int? NumeroEpisodio { get; set; }
        public int? NumeroTemporada { get; set; }
        public int? CantTemporadas { get; set; }
    }

}
