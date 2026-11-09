using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Application.Interfaces.Grupo;

public interface IGrupoServicio
{
    Task<GrupoDTO> AddAsync(GrupoDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<GrupoDTO?> GetAsync(int id);
    Task<IEnumerable<GrupoDTO>> GetAllAsync();
    Task<bool> UpdateAsync(GrupoDTO dto);
}
