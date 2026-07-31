# Trabajo Práctico Integrador para .NET

## Integrantes
* Julia Aguaya - Legajo 49459
* Martín Quagliardi - Legajo 51657
* Laureano Toloza - Legajo 51871

## Enunciado
El trabajo consiste en una aplicación para realizar reseñas y comentarios sobre distintos tipos de contenido ya sean películas, series, o episodios puntuales. Los usuarios pueden crear un perfil y empezar a reseñar un contenido que hayan visto y recibir un feedback por parte de los otros usuarios en forma de comentarios.

En este trabajo usamos la API de TMDB para que los usuarios puedan buscar películas o series que no existan ya en nuestra base de datos.

## Modelo
```mermaid
erDiagram
    Contenido ||--|| Episodio : "Es"
    Contenido ||--|| Serie : "Es"
    Contenido ||--|| Pelicula : "Es"
    Contenido ||--o{ Reseña : Contiene
    Reseña ||--o{ Comentario : Tiene
    Usuario ||--o{ Reseña : Escribe
    Usuario ||--o{ Comentario : Responde
    Usuario ||--|| Rol : "Pertenece a"
    Serie ||--o{ Episodio : "Contiene"


Contenido{
    int id
    int idTMDB
    string Tipo
    %% La ID del contenido en la API
    string Nombre
    string Descripcion
    string Genero
    string NombreDirector
    date FechaLanzamiento
    time Duracion
    string urlImagen
    int puntuacionPromedio
}

Reseña{
    int id
    date FechaPublicacion
    string Texto
    int Calificacion
    int cantReacciones
}

Usuario{
    int id
    string NombreUsuario
    string Contraseña
    string Mail
}

Rol {
    int id
    int Denominacion
}

Comentario {
    string Texto
    date FechaRealizacion
    string Reaccion
}

Serie {
    int CantidadTemporadas
}

Pelicula {
}

Episodio {
    int numeroEpisodio
    int numeroTemporada
}
```