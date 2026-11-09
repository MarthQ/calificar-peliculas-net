using System.Security.Claims;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Comentarios;
using CalificarPeliculas.Web.Security;

namespace CalificarPeliculas.Web;

public static class ComentarioEndpoints
{
    public static void MapComentarioEndpoints(this WebApplication app)
    {
        app.MapGet("/comentarios/{id}", async (int id, IComentarioServicio servicio) =>
        {
            var dto = await servicio.GetAsync(id);
            return dto == null ? Results.NotFound() : Results.Ok(dto);
        }).RequireAuthorization();

        app.MapGet("/comentarios", async (IComentarioServicio servicio) =>
            Results.Ok(await servicio.GetAllAsync())).RequireAuthorization();

        app.MapPost("/comentarios", async (ComentarioDTO dto, ClaimsPrincipal user, IComentarioServicio servicio) =>
        {
            if (!UsuarioActual.EsGestion(user))
                dto.UsuarioId = UsuarioActual.Id(user);

            try
            {
                var creado = await servicio.AddAsync(dto);
                return Results.Created($"/comentarios/{creado.Id}", creado);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).RequireAuthorization();

        app.MapPut("/comentarios", async (ComentarioDTO dto, ClaimsPrincipal user, IComentarioServicio servicio) =>
        {
            var existente = await servicio.GetAsync(dto.Id);
            if (existente == null) return Results.NotFound();
            if (!UsuarioActual.EsGestion(user))
            {
                if (existente.UsuarioId != UsuarioActual.Id(user)) return Results.Forbid();
                dto.UsuarioId = existente.UsuarioId;
            }

            try
            {
                return await servicio.UpdateAsync(dto) ? Results.NoContent() : Results.NotFound();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).RequireAuthorization();

        app.MapDelete("/comentarios/{id}", async (int id, ClaimsPrincipal user, IComentarioServicio servicio) =>
        {
            var existente = await servicio.GetAsync(id);
            if (existente == null) return Results.NotFound();
            if (!UsuarioActual.EsGestion(user) && existente.UsuarioId != UsuarioActual.Id(user)) return Results.Forbid();
            await servicio.DeleteAsync(id);
            return Results.NoContent();
        }).RequireAuthorization();
    }
}
