using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalificarPeliculas.Domain
{
    public class Episodio : Contenido
    {
        public int NumeroEpisodio { get; private set; }
        public int NumeroTemporada { get; private set; }

        protected Episodio() { }
        public Episodio(int id, int idTMDB, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio, int? numep, int? numtemp)
            : base(id, idTMDB, TipoContenido.EPISODIO, nombre, generoid, descripcion, nombreDirector, fechaLanzamiento, duracion, urlImagen, puntuacionPromedio)
        {
            SetNumEp(numep);
            SetNumTemp(numtemp);
        }
        
        public void SetNumEp(int? numep)
        {

            ArgumentNullException.ThrowIfNull(numep, nameof(numep));
            if (numep < 0)
                throw new ArgumentException("El número de episodio no puede ser negativo.", nameof(numep));
            NumeroEpisodio = (int)numep;
        }
        public void SetNumTemp(int? numtemp)
        {
            ArgumentNullException.ThrowIfNull(numtemp, nameof(numtemp));
            if (numtemp < 0)
                throw new ArgumentException("El número de temporada no puede ser negativo.", nameof(numtemp));
            NumeroTemporada = (int)numtemp;
        }
    }
}
