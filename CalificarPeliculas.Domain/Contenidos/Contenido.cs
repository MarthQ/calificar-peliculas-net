namespace CalificarPeliculas.Domain.Contenidos
{
    public class Contenido
    {
        private int id;
        private int idTMDB;
        public string tipo;
        public string nombre;
        public string descripcion;
        public string nombreDirector;
        public DateOnly fechaLanzamiento;
        public TimeOnly duracion;
        public string urlImagen;
        public float puntuacionPromedio;
    }
}
