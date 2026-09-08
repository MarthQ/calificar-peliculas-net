namespace CalificarPeliculas.Domain
{
    public abstract class Contenido
    {
        public int Id { get; private set; }
        public int IdTMDB { get; private set; }
        public TipoContenido Tipo { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string NombreDirector { get; private set; }
        public DateOnly FechaLanzamiento { get; private set; }
        public TimeOnly Duracion { get; private set; }
        public string UrlImagen { get; private set; }
        public float PuntuacionPromedio { get; private set; }
        // Privados para evitar cambios directos que desincronicen el género y su ID.
        private Genero? _genero;
        private int _generoId;

        public int GeneroId
        {
            get => _genero?.Id ?? _generoId;
            private set => _generoId = value;
        }

        public Genero? Genero
        {
            get => _genero;
            private set
            {
                _genero = value;
                if (value != null && _generoId != value.Id)
                {
                    _generoId = value.Id;
                }
            }
        }

        protected Contenido() { }

        protected Contenido(int id, int idTMDB, TipoContenido tipo, string nombre, int generoid, string descripcion, string nombreDirector, DateOnly fechaLanzamiento, TimeOnly duracion, string urlImagen, float puntuacionPromedio)
        {
            SetId(id);
            SetIdTMDB(idTMDB);
            SetTipo(tipo);
            SetNombre(nombre);
            SetGeneroId(generoid);
            SetDescripcion(descripcion);
            SetNombreDirector(nombreDirector);
            SetFechaLanzamiento(fechaLanzamiento);
            SetDuracion(duracion);
            SetUrlImagen(urlImagen);
            SetPuntuacionPromedio(puntuacionPromedio);
        }
        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("La ID debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }
        public void SetIdTMDB(int idtmdb)
        {
            if (idtmdb < 0)
                throw new ArgumentException("La ID debe ser mayor o igual a 0.", nameof(idtmdb));
            IdTMDB = idtmdb;
        }
        private void SetTipo(TipoContenido tipo)
        {
            if (!Enum.IsDefined(tipo))
                throw new ArgumentException("El tipo de contenido no es válido.", nameof(tipo));
            Tipo = tipo;
        }
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser vacío o nulo.", nameof(nombre));
            Nombre = nombre;
        }
        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede ser vacía o nula.", nameof(descripcion));
            Descripcion = descripcion;
        }
        public void SetNombreDirector(string nombreDirector)
        {
            if (string.IsNullOrWhiteSpace(nombreDirector))
                throw new ArgumentException("El nombre del director no puede ser vacío o nulo.", nameof(nombreDirector));
            NombreDirector = nombreDirector;
        }
        public void SetFechaLanzamiento(DateOnly fecha)
        {
            if (fecha == default)
                throw new ArgumentException("La fecha de lanzamiento no puede ser la fecha por defecto.", nameof(fecha));
            FechaLanzamiento = fecha;
        }
        public void SetDuracion(TimeOnly duracion)
        {
            if (duracion == default)
                throw new ArgumentException("La duración no puede ser el valor por defecto.", nameof(duracion));
            Duracion = duracion;
        }
        public void SetUrlImagen(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("La URL de la imagen no puede ser vacía o nula.", nameof(url));
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ArgumentException("La URL de la imagen no es una URL válida.", nameof(url));
            UrlImagen = url;
        }
        public void SetPuntuacionPromedio(float puntuacion)
        {
            if (puntuacion < 0f || puntuacion > 10f)
                throw new ArgumentException("La puntuación promedio debe estar entre 0 y 10.", nameof(puntuacion));
            PuntuacionPromedio = puntuacion;
        }
        public void SetGenero(Genero genero)
        {
            ArgumentNullException.ThrowIfNull(genero);
            _genero = genero;
            _generoId = genero.Id;
        }
        public void SetGeneroId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El id del género debe ser mayor o igual a 0.", nameof(id));
            _generoId = id;

            if (_genero != null && _genero.Id!= id)
            {
                _genero = null;
            }
        }
    }
}
