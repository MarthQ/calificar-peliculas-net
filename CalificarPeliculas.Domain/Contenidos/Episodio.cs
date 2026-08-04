namespace CalificarPeliculas.Domain.Contenidos
{
    public class Episodio(int id, int idTMDB, string tipo, string nombre, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio, int numeroEpisodio, int numeroTemporada) : Contenido(id, idTMDB, tipo, nombre, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
    {
        public int NumeroEpisodio { get; set; } = numeroEpisodio;
        public int NumeroTemporada { get; set; } = numeroTemporada;
    }
}
