namespace CalificarPeliculas.Domain.Contenidos
{
    public class Pelicula(int id, int idTMDB, string tipo, string nombre, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio) : Contenido(id, idTMDB, tipo, nombre, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
    {
    }
}
