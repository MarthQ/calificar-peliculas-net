using CalificarPeliculas.Application.Interfaces.Grupo;
using GrupoEntidad = CalificarPeliculas.Domain.Users.Grupo;
using Microsoft.EntityFrameworkCore;

namespace CalificarPeliculas.Repository;

public class GrupoRepository : IGrupoRepository
{
    private CFContext CreateContext() => new();

    public async Task AddAsync(GrupoEntidad grupo)
    {
        using var context = CreateContext();
        context.Grupos.Add(grupo);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var context = CreateContext();
        var grupo = await context.Grupos.FindAsync(id);
        if (grupo == null) return false;
        context.Grupos.Remove(grupo);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<GrupoEntidad?> GetAsync(int id)
    {
        using var context = CreateContext();
        return await context.Grupos.FindAsync(id);
    }

    public async Task<IEnumerable<GrupoEntidad>> GetAllAsync()
    {
        using var context = CreateContext();
        return await context.Grupos.ToListAsync();
    }

    public async Task<bool> UpdateAsync(GrupoEntidad grupo)
    {
        using var context = CreateContext();
        var existing = await context.Grupos.FindAsync(grupo.Id);
        if (existing == null) return false;
        existing.SetNombre(grupo.Nombre);
        existing.SetDescripcion(grupo.Descripcion);
        existing.SetFechaCreacion(grupo.FechaCreacion);
        await context.SaveChangesAsync();
        return true;
    }
}
