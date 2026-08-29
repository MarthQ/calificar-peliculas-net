namespace CalificarPeliculas.Domain
{
    public class Pelicula : Contenido
    {

        protected Pelicula() { }
        public Pelicula(int id, int idTMDB, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio)
            : base(id, idTMDB, TipoContenido.PELICULA, nombre, generoid, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
        {
        }
    }
}
