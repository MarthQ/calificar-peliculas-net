namespace CalificarPeliculas.Domain
{
    public class Episodio : Contenido
    {
        public int NumeroEpisodio { get; private set; }
        public int NumeroTemporada { get; private set; }

        public Episodio(int id, int idTMDB, string tipo, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio, int numep, int numtemp) : base(id, idTMDB, tipo, nombre, generoid, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
        {
            SetNumEp(numep);
            SetNumTemp(numtemp);
        }
        
        public void SetNumEp(int numep)
        {
            if (numep < 0)
                throw new ArgumentException("El número de episodio no puede ser negativo.", nameof(numep));
            NumeroEpisodio = numep;
        }
        public void SetNumTemp(int numtemp)
        {
            if (numtemp < 0)
                throw new ArgumentException("El número de temporada no puede ser negativo.", nameof(numtemp));
            NumeroTemporada = numtemp;
        }
    }
}
