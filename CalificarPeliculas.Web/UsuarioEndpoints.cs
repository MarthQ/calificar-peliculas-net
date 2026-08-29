using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Usuario;

namespace CalificarPeliculas.Web
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            app.MapGet("/usuarios/{id}", async (int id, IUsuarioServicio usuarioServicio) =>
            {
                var usuario = await usuarioServicio.GetByIdAsync(id);
                return usuario == null ? Results.NotFound() : Results.Ok(usuario);
            })
            .WithName("GetUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/usuarios", async (IUsuarioServicio usuarioServicio) =>
            {
                return Results.Ok(await usuarioServicio.GetAllAsync());
            })
            .WithName("GetAllUsuarios")
            .Produces<IEnumerable<UsuarioDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/usuarios", async (RegistrarUsuarioDTO dto, IUsuarioServicio usuarioServicio) =>
            {
                try
                {
                    var usuario = await usuarioServicio.RegistrarAsync(dto);
                    return Results.Created($"/usuarios/{usuario.Id}", usuario);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("RegistrarUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            app.MapPut("/usuarios", async (ActualizarUsuarioDTO dto, IUsuarioServicio usuarioServicio) =>
            {
                try
                {
                    var actualizado = await usuarioServicio.UpdateAsync(dto);
                    return actualizado ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateUsuario")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            app.MapDelete("/usuarios/{id}", async (int id, IUsuarioServicio usuarioServicio) =>
            {
                var eliminado = await usuarioServicio.DeleteAsync(id);
                return eliminado ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteUsuario")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/usuarios/login", async (LoginUsuarioDTO dto, IUsuarioServicio usuarioServicio) =>
            {
                var usuario = await usuarioServicio.LoginAsync(dto);
                return usuario == null ? Results.Unauthorized() : Results.Ok(usuario);
            })
            .WithName("LoginUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }
}
