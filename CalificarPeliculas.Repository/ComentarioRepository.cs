using CalificarPeliculas.Application.Interfaces.Comentarios;
using CalificarPeliculas.Domain;
using Microsoft.EntityFrameworkCore;

namespace CalificarPeliculas.Repository;

public class ComentarioRepository : IComentarioRepository
{
    private CFContext CreateContext() => new();

    public async Task AddAsync(Comentario comentario)
    {
        using var context = CreateContext();
        context.Comentarios.Add(comentario);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var context = CreateContext();
        var comentario = await context.Comentarios.FindAsync(id);
        if (comentario == null) return false;
        context.Comentarios.Remove(comentario);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Comentario?> GetAsync(int id)
    {
        using var context = CreateContext();
        return await context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comentario>> GetAllAsync()
    {
        using var context = CreateContext();
        return await context.Comentarios.ToListAsync();
    }

    public async Task<bool> UpdateAsync(Comentario comentario)
    {
        using var context = CreateContext();
        var existing = await context.Comentarios.FirstOrDefaultAsync(c => c.Id == comentario.Id);
        if (existing == null) return false;
        existing.SetCriticaId(comentario.CriticaId);
        existing.SetUsuarioId(comentario.UsuarioId);
        existing.SetTexto(comentario.Texto);
        existing.SetFechaRealizacion(comentario.FechaRealizacion);
        existing.SetReaccion(comentario.Reaccion);
        await context.SaveChangesAsync();
        return true;
    }
}
