using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Application.DTOs;
namespace CalificarPeliculas.Web
{
    public static class GeneroEndpoints
    {
        public static void MapGeneroEndpoints(this WebApplication app)
        {
            app.MapGet("/generos/{id}", async (int id, IGeneroServicio generoServicio) =>
            {
                GeneroDTO? dto = await generoServicio.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetGenero")
            .Produces<GeneroDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/generos", async (IGeneroServicio generoServicio) =>
            {
                var dtos = await generoServicio.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllGeneros")
            .Produces<List<GeneroDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/generos", async (GeneroDTO dto, IGeneroServicio generoServicio) =>
            {
                try
                {
                    GeneroDTO generoDTO = await generoServicio.AddAsync(dto);

                    return Results.Created($"/generos/{generoDTO.Id}", generoDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddGenero")
            .Produces<GeneroDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/generos", async (GeneroDTO dto, IGeneroServicio generoServicio) =>
            {
                try
                {
                    var found = await generoServicio.UpdateAsync(dto);

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
            .WithName("UpdateGenero")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/generos/{id}", async (int id, IGeneroServicio generoServicio) =>
            {
                var deleted = await generoServicio.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteGenero")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
