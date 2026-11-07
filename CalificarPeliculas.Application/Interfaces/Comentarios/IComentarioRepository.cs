using CalificarPeliculas.Domain;

namespace CalificarPeliculas.Application.Interfaces.Comentarios;

public interface IComentarioRepository
{
    Task AddAsync(Comentario comentario);
    Task<bool> DeleteAsync(int id);
    Task<Comentario?> GetAsync(int id);
    Task<IEnumerable<Comentario>> GetAllAsync();
    Task<bool> UpdateAsync(Comentario comentario);
}
