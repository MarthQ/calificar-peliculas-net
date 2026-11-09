using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Comentarios;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Application.Interfaces.Usuario;

namespace CalificarPeliculas.Web;

public static class ReporteEndpoints
{
    public static void MapReporteEndpoints(this WebApplication app)
    {
        app.MapGet("/reportes/contenidos/resumen", async (
            IContenidoServicio contenidos,
            IGeneroServicio generos,
            ICriticaServicio criticas,
            IComentarioServicio comentarios) =>
        {
            var contenidosData = (await contenidos.GetAllAsync()).ToList();
            var generosData = (await generos.GetAllAsync()).ToDictionary(g => g.Id, g => g.Nombre);
            var criticasData = (await criticas.GetAllAsync()).ToList();
            var comentariosData = (await comentarios.GetAllAsync()).ToList();

            var resultado = contenidosData.Select(contenido =>
            {
                var criticasContenido = criticasData.Where(c => c.ContenidoId == contenido.Id).ToList();
                var criticaIds = criticasContenido.Select(c => c.Id).ToHashSet();
                return new ReporteContenidoDTO
                {
                    ContenidoId = contenido.Id,
                    Contenido = contenido.Nombre,
                    Genero = generosData.GetValueOrDefault(contenido.GeneroId, "Sin género"),
                    Tipo = contenido.Tipo.ToString(),
                    PromedioCalificacion = criticasContenido.Count == 0 ? 0 : criticasContenido.Average(c => c.Calificacion),
                    CantidadCriticas = criticasContenido.Count,
                    CantidadComentarios = comentariosData.Count(c => criticaIds.Contains(c.CriticaId))
                };
            }).OrderByDescending(r => r.PromedioCalificacion).ToList();

            return Results.Ok(resultado);
        }).RequireAuthorization("Gestion");

        app.MapGet("/reportes/contenidos/detalle", async (
            IContenidoServicio contenidos,
            IGeneroServicio generos,
            ICriticaServicio criticas,
            IComentarioServicio comentarios,
            IUsuarioServicio usuarios) =>
        {
            var contenidosData = (await contenidos.GetAllAsync()).ToDictionary(c => c.Id);
            var generosData = (await generos.GetAllAsync()).ToDictionary(g => g.Id, g => g.Nombre);
            var usuariosData = (await usuarios.GetAllAsync()).ToDictionary(u => u.Id, u => u.NombreUsuario);
            var comentariosData = (await comentarios.GetAllAsync()).ToList();
            var resultado = new List<ReporteDetalleDTO>();

            foreach (var critica in await criticas.GetAllAsync())
            {
                if (!contenidosData.TryGetValue(critica.ContenidoId, out var contenido)) continue;
                var comentariosCritica = comentariosData.Where(c => c.CriticaId == critica.Id).ToList();
                var genero = generosData.GetValueOrDefault(contenido.GeneroId, "Sin género");
                var usuario = usuariosData.GetValueOrDefault(critica.UsuarioId, critica.UsuarioId.ToString());

                if (comentariosCritica.Count == 0)
                {
                    resultado.Add(CrearDetalle(contenido, genero, usuario, critica, null));
                }
                else
                {
                    foreach (var comentario in comentariosCritica)
                        resultado.Add(CrearDetalle(contenido, genero, usuario, critica, comentario));
                }
            }

            return Results.Ok(resultado.OrderBy(r => r.Contenido).ThenByDescending(r => r.FechaCritica));
        }).RequireAuthorization("Gestion");
    }

    private static ReporteDetalleDTO CrearDetalle(
        ContenidoDTO contenido,
        string genero,
        string usuario,
        CriticaDTO critica,
        ComentarioDTO? comentario) => new()
    {
        Contenido = contenido.Nombre,
        Genero = genero,
        Tipo = contenido.Tipo.ToString(),
        Usuario = usuario,
        TextoCritica = critica.Texto,
        Calificacion = critica.Calificacion,
        FechaCritica = critica.FechaPublicacion,
        TextoComentario = comentario?.Texto ?? string.Empty,
        Reaccion = comentario?.Reaccion ?? string.Empty,
        FechaComentario = comentario?.FechaRealizacion
    };
}
