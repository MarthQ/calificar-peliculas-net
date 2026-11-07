using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Comentarios;

namespace CalificarPeliculas.Web;

public static class ComentarioEndpoints
{
    public static void MapComentarioEndpoints(this WebApplication app)
    {
        app.MapGet("/comentarios/{id}", async (int id, IComentarioServicio servicio) =>
        {
            var dto = await servicio.GetAsync(id);
            return dto == null ? Results.NotFound() : Results.Ok(dto);
        }).RequireAuthorization("Admin");

        app.MapGet("/comentarios", async (IComentarioServicio servicio) =>
            Results.Ok(await servicio.GetAllAsync())).RequireAuthorization("Admin");

        app.MapPost("/comentarios", async (ComentarioDTO dto, IComentarioServicio servicio) =>
        {
            try
            {
                var creado = await servicio.AddAsync(dto);
                return Results.Created($"/comentarios/{creado.Id}", creado);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).RequireAuthorization("Admin");

        app.MapPut("/comentarios", async (ComentarioDTO dto, IComentarioServicio servicio) =>
        {
            try
            {
                return await servicio.UpdateAsync(dto) ? Results.NoContent() : Results.NotFound();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).RequireAuthorization("Admin");

        app.MapDelete("/comentarios/{id}", async (int id, IComentarioServicio servicio) =>
            await servicio.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
            .RequireAuthorization("Admin");
    }
}
