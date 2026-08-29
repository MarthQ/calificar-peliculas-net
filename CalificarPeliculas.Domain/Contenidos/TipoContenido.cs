namespace CalificarPeliculas.Domain
{
    // Centraliza los únicos tipos válidos para evitar cadenas inconsistentes entre API, dominio y persistencia.
    public enum TipoContenido
    {
        EPISODIO = 1,
        PELICULA = 2,
        SERIE = 3
    }
}
