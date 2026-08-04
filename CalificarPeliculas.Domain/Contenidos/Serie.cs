namespace CalificarPeliculas.Domain.Contenidos
{
    public class Serie(int id, int idTMDB, string tipo, string nombre, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio, int cantTemporadas) : Contenido(id, idTMDB, tipo, nombre, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
    {
        public int CantTemporadas { get; set; } = cantTemporadas;
    }
}
