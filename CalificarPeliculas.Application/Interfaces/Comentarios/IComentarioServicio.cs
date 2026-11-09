using CalificarPeliculas.Application.DTOs;

namespace CalificarPeliculas.Application.Interfaces.Comentarios;

public interface IComentarioServicio
{
    Task<ComentarioDTO> AddAsync(ComentarioDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<ComentarioDTO?> GetAsync(int id);
    Task<IEnumerable<ComentarioDTO>> GetAllAsync();
    Task<bool> UpdateAsync(ComentarioDTO dto);
}
