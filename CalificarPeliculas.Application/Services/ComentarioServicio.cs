using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Comentarios;
using CalificarPeliculas.Domain;

namespace CalificarPeliculas.Application.Services;

public class ComentarioServicio : IComentarioServicio
{
    private readonly IComentarioRepository repository;

    public ComentarioServicio(IComentarioRepository repository) => this.repository = repository;

    public async Task<ComentarioDTO> AddAsync(ComentarioDTO dto)
    {
        var comentario = new Comentario(0, dto.CriticaId, dto.UsuarioId, dto.Texto, dto.FechaRealizacion, dto.Reaccion);
        await repository.AddAsync(comentario);
        return Map(comentario);
    }

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);

    public async Task<ComentarioDTO?> GetAsync(int id)
    {
        var comentario = await repository.GetAsync(id);
        return comentario == null ? null : Map(comentario);
    }

    public async Task<IEnumerable<ComentarioDTO>> GetAllAsync() =>
        (await repository.GetAllAsync()).Select(Map).ToList();

    public Task<bool> UpdateAsync(ComentarioDTO dto) => repository.UpdateAsync(
        new Comentario(dto.Id, dto.CriticaId, dto.UsuarioId, dto.Texto, dto.FechaRealizacion, dto.Reaccion));

    private static ComentarioDTO Map(Comentario comentario) => new()
    {
        Id = comentario.Id,
        CriticaId = comentario.CriticaId,
        UsuarioId = comentario.UsuarioId,
        Texto = comentario.Texto,
        FechaRealizacion = comentario.FechaRealizacion,
        Reaccion = comentario.Reaccion
    };
}
