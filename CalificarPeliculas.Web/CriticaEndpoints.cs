using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Criticas;

namespace CalificarPeliculas.Web
{
    public static class CriticaEndpoints
    {
        public static void MapCriticaEndpoints(this WebApplication app)
        {
            app.MapGet("/criticas/{id}", async (int id, ICriticaServicio criticaServicio) =>
            {
                CriticaDTO? dto = await criticaServicio.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetCritica")
            .Produces<CriticaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/criticas", async (ICriticaServicio criticaServicio) =>
            {
                var dtos = await criticaServicio.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllCriticas")
            .Produces<List<CriticaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/criticas", async (CriticaDTO dto, ICriticaServicio criticaServicio) =>
            {
                try
                {
                    CriticaDTO criticaDTO = await criticaServicio.AddAsync(dto);

                    return Results.Created($"/criticas/{criticaDTO.Id}", criticaDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddCritica")
            .Produces<CriticaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPut("/criticas", async (CriticaDTO dto, ICriticaServicio criticaServicio) =>
            {
                try
                {
                    var found = await criticaServicio.UpdateAsync(dto);

                    if (!found)
                    {
                        return Results.NotFound();
                    }

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateCritica")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapDelete("/criticas/{id}", async (int id, ICriticaServicio criticaServicio) =>
            {
                var deleted = await criticaServicio.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteCritica")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();
        }
    }
}
