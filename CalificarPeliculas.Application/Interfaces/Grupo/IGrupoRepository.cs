using GrupoEntidad = CalificarPeliculas.Domain.Users.Grupo;

namespace CalificarPeliculas.Application.Interfaces.Grupo;

public interface IGrupoRepository
{
    Task AddAsync(GrupoEntidad grupo);
    Task<bool> DeleteAsync(int id);
    Task<GrupoEntidad?> GetAsync(int id);
    Task<IEnumerable<GrupoEntidad>> GetAllAsync();
    Task<bool> UpdateAsync(GrupoEntidad grupo);
}
