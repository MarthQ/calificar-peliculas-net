using System.Text.Json.Serialization;

namespace CalificarPeliculas.Domain
{
    // Centraliza los únicos tipos válidos para evitar cadenas inconsistentes entre API, dominio y persistencia.
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoContenido
    {
        EPISODIO = 1,
        PELICULA = 2,
        SERIE = 3
    }
}
