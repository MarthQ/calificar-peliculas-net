using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Grupo;
using GrupoEntidad = CalificarPeliculas.Domain.Users.Grupo;

namespace CalificarPeliculas.Application.Services;

public class GrupoServicio : IGrupoServicio
{
    private readonly IGrupoRepository repository;

    public GrupoServicio(IGrupoRepository repository) => this.repository = repository;

    public async Task<GrupoDTO> AddAsync(GrupoDTO dto)
    {
        var grupo = new GrupoEntidad(0, dto.Nombre, dto.Descripcion, dto.FechaCreacion);
        await repository.AddAsync(grupo);
        return Map(grupo);
    }

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);

    public async Task<GrupoDTO?> GetAsync(int id)
    {
        var grupo = await repository.GetAsync(id);
        return grupo == null ? null : Map(grupo);
    }

    public async Task<IEnumerable<GrupoDTO>> GetAllAsync() =>
        (await repository.GetAllAsync()).Select(Map).ToList();

    public Task<bool> UpdateAsync(GrupoDTO dto) => repository.UpdateAsync(
        new GrupoEntidad(dto.Id, dto.Nombre, dto.Descripcion, dto.FechaCreacion));

    private static GrupoDTO Map(GrupoEntidad grupo) => new()
    {
        Id = grupo.Id,
        Nombre = grupo.Nombre,
        Descripcion = grupo.Descripcion,
        FechaCreacion = grupo.FechaCreacion
    };
}
