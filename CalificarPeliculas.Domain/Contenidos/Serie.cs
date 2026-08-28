namespace CalificarPeliculas.Domain
{
    public class Serie : Contenido
    {
        public int CantTemporadas { get; private set; }

        public Serie(int id, int idTMDB, string tipo, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio, int cantTemporadas)
            : base(id, idTMDB, tipo, nombre, generoid, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
        {
            SetCantTemporadas(cantTemporadas);
        }

        public void SetCantTemporadas(int cant)
        {
            if (cant < 0)
                throw new ArgumentException("La cantidad de temporadas debe ser mayor o igual a 0.", nameof(cant));
            CantTemporadas = cant;
        }
    }
}
