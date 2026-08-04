namespace CalificarPeliculas.Domain.Contenidos
{
    public abstract class Contenido(int id, int idTMDB, string tipo, string nombre, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio)
    {
        private int Id { get; set; } = id;
        private int IdTMDB { get; set; } = idTMDB;
        public string? Tipo { get; set; } = tipo;
        public string? Nombre { get; set; } = nombre;
        public string? Descripcion { get; set; } = descripcion;
        public string? NombreDirector { get; set; } = nombreDirector;
        public DateOnly FechaLanzamiento { get; set; } = fechaLanzamiento;
        public TimeOnly Duracion { get; set; } = duracion;
        public string? UrlImagen { get; set; } = urlImagen;
        public float PuntuacionPromedio { get; set; } = puntuacionPromedio;
    }
}
