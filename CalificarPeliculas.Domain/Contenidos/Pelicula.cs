namespace CalificarPeliculas.Domain
{
    public class Pelicula : Contenido
    {
        public Pelicula(int id, int idTMDB, string tipo, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio)
            : base(id, idTMDB, tipo, nombre, generoid, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
        {
        }
    }
}
