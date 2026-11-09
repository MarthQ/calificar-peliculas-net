using System.Security.Claims;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Web.Security;

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
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/criticas", async (ICriticaServicio criticaServicio) =>
            {
                var dtos = await criticaServicio.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllCriticas")
            .Produces<List<CriticaDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPost("/criticas", async (CriticaDTO dto, ClaimsPrincipal user, ICriticaServicio criticaServicio) =>
            {
                if (!UsuarioActual.EsGestion(user))
                {
                    dto.UsuarioId = UsuarioActual.Id(user);
                }

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

            app.MapPut("/criticas", async (CriticaDTO dto, ClaimsPrincipal user, ICriticaServicio criticaServicio) =>
            {
                var existente = await criticaServicio.GetAsync(dto.Id);
                if (existente == null)
                {
                    return Results.NotFound();
                }

                if (!UsuarioActual.EsGestion(user) && existente.UsuarioId != UsuarioActual.Id(user))
                {
                    return Results.Forbid();
                }

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
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapDelete("/criticas/{id}", async (int id, ClaimsPrincipal user, ICriticaServicio criticaServicio) =>
            {
                var existente = await criticaServicio.GetAsync(id);
                if (existente == null)
                {
                    return Results.NotFound();
                }

                if (!UsuarioActual.EsGestion(user) && existente.UsuarioId != UsuarioActual.Id(user))
                {
                    return Results.Forbid();
                }

                await criticaServicio.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleteCritica")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization()
            .WithOpenApi();
        }
    }
}
