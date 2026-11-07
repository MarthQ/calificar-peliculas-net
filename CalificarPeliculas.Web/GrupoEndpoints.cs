using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Grupo;

namespace CalificarPeliculas.Web;

public static class GrupoEndpoints
{
    public static void MapGrupoEndpoints(this WebApplication app)
    {
        app.MapGet("/grupos/{id}", async (int id, IGrupoServicio servicio) =>
        {
            var dto = await servicio.GetAsync(id);
            return dto == null ? Results.NotFound() : Results.Ok(dto);
        }).RequireAuthorization("Admin");

        app.MapGet("/grupos", async (IGrupoServicio servicio) =>
            Results.Ok(await servicio.GetAllAsync())).RequireAuthorization("Admin");

        app.MapPost("/grupos", async (GrupoDTO dto, IGrupoServicio servicio) =>
        {
            try
            {
                var creado = await servicio.AddAsync(dto);
                return Results.Created($"/grupos/{creado.Id}", creado);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).RequireAuthorization("Admin");

        app.MapPut("/grupos", async (GrupoDTO dto, IGrupoServicio servicio) =>
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

        app.MapDelete("/grupos/{id}", async (int id, IGrupoServicio servicio) =>
            await servicio.DeleteAsync(id) ? Results.NoContent() : Results.NotFound())
            .RequireAuthorization("Admin");
    }
}
