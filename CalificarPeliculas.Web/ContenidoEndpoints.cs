using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Application.DTOs;
namespace CalificarPeliculas.Web
{
    public static class ContenidoEndpoints
    {
        public static void MapContenidoEndpoints(this WebApplication app)
        {
            app.MapGet("/contenidos/{id}", async (int id, IContenidoServicio contenidoServicio) =>
            {
                ContenidoDTO? dto = await contenidoServicio.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetContenido")
            .Produces<ContenidoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/contenidos", async (IContenidoServicio contenidoServicio) =>
            {
                var dtos = await contenidoServicio.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllContenidos")
            .Produces<List<ContenidoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/contenidos", async (ContenidoDTO dto, IContenidoServicio contenidoServicio) =>
            {
                try
                {
                    ContenidoDTO contenidoDTO = await contenidoServicio.AddAsync(dto);

                    return Results.Created($"/contenidos/{contenidoDTO.Id}", contenidoDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddContenido")
            .Produces<ContenidoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/contenidos", async (ContenidoDTO dto, IContenidoServicio contenidoServicio) =>
            {
                try
                {
                    var found = await contenidoServicio.UpdateAsync(dto);

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
            .WithName("UpdateContenido")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/contenidos/{id}", async (int id, IContenidoServicio contenidoServicio) =>
            {
                var deleted = await contenidoServicio.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteContenido")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
