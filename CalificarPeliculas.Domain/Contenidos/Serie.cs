namespace CalificarPeliculas.Domain
{
    public class Serie : Contenido
    {
        public int CantTemporadas { get; private set; }

        protected Serie() { }
        public Serie(int id, int idTMDB, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio, int? cantTemporadas)
            : base(id, idTMDB, TipoContenido.SERIE, nombre, generoid, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
        {
            SetCantTemporadas(cantTemporadas);
        }

        public void SetCantTemporadas(int? cant)
        {
            ArgumentNullException.ThrowIfNull(cant, nameof(cant));
            if (cant < 0)
                throw new ArgumentException("La cantidad de temporadas debe ser mayor o igual a 0.", nameof(cant));
            CantTemporadas = (int)cant;
        }
    }
}
