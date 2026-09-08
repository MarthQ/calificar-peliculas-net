using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Services
{
    public class ContenidoServicio : IContenidoServicio
    {
        private readonly IContenidoRepository contenidoRepository;
        public ContenidoServicio(IContenidoRepository contenidoRepository)
        {
            this.contenidoRepository = contenidoRepository;
        }

        public async Task<ContenidoDTO> AddAsync(ContenidoDTO dto)
        {

            Contenido contenido = dto.Tipo switch
            {
                TipoContenido.EPISODIO => new Episodio(0, dto.IdTMDB, dto.Nombre, dto.GeneroId, dto.Descripcion, dto.NombreDirector, dto.FechaLanzamiento, dto.Duracion, dto.UrlImagen, dto.PuntuacionPromedio, dto.NumeroEpisodio, dto.NumeroTemporada),
                TipoContenido.PELICULA => new Pelicula(0, dto.IdTMDB, dto.Nombre, dto.GeneroId, dto.Descripcion, dto.NombreDirector, dto.FechaLanzamiento, dto.Duracion, dto.UrlImagen, dto.PuntuacionPromedio),
                TipoContenido.SERIE => new Serie(0, dto.IdTMDB, dto.Nombre, dto.GeneroId, dto.Descripcion, dto.NombreDirector, dto.FechaLanzamiento, dto.Duracion, dto.UrlImagen, dto.PuntuacionPromedio, dto.CantTemporadas),
                _ => throw new ArgumentException("Tipo de Contenido no válido.")
            };

            await contenidoRepository.AddAsync(contenido);
            dto = MapToDTO(contenido);
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await contenidoRepository.DeleteAsync(id);
        }

        public async Task<ContenidoDTO?> GetAsync(int id)
        {
            Contenido? contenido = await contenidoRepository.GetAsync(id);

            if (contenido == null)
                return null;

            return MapToDTO(contenido);
        }

        public async Task<IEnumerable<ContenidoDTO>> GetAllAsync()
        {
            var contenidos = await contenidoRepository.GetAllAsync();

            return contenidos.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(ContenidoDTO dto)
        {
            Contenido contenido = dto.Tipo switch
            {
                TipoContenido.EPISODIO => new Episodio(dto.Id, dto.IdTMDB, dto.Nombre, dto.GeneroId, dto.Descripcion, dto.NombreDirector, dto.FechaLanzamiento, dto.Duracion, dto.UrlImagen, dto.PuntuacionPromedio, dto.NumeroEpisodio, dto.NumeroTemporada),
                TipoContenido.PELICULA => new Pelicula(dto.Id, dto.IdTMDB, dto.Nombre, dto.GeneroId, dto.Descripcion, dto.NombreDirector, dto.FechaLanzamiento, dto.Duracion, dto.UrlImagen, dto.PuntuacionPromedio),
                TipoContenido.SERIE => new Serie(dto.Id, dto.IdTMDB, dto.Nombre, dto.GeneroId, dto.Descripcion, dto.NombreDirector, dto.FechaLanzamiento, dto.Duracion, dto.UrlImagen, dto.PuntuacionPromedio, dto.CantTemporadas),
                _ => throw new ArgumentException("Tipo de Contenido no válido.")
            };
            return await contenidoRepository.UpdateAsync(contenido);
        }

        private static ContenidoDTO MapToDTO(Contenido contenido)
        {

            var dto = new ContenidoDTO {
                Id = contenido.Id,
                IdTMDB = contenido.IdTMDB,
                Nombre = contenido.Nombre,
                GeneroId = contenido.GeneroId,
                Descripcion = contenido.Descripcion,
                NombreDirector = contenido.NombreDirector,
                FechaLanzamiento = contenido.FechaLanzamiento,
                Duracion = contenido.Duracion,
                UrlImagen = contenido.UrlImagen,
                PuntuacionPromedio = contenido.PuntuacionPromedio,
                Tipo = contenido.Tipo
            };
            switch (contenido) {

                case Episodio e:
                    dto.NumeroEpisodio = e.NumeroEpisodio;
                    dto.NumeroTemporada = e.NumeroTemporada;
                break;

                case Pelicula:
                    break;

                case Serie s:
                    dto.CantTemporadas = s.CantTemporadas;
                    break;

            }
            return dto;
    }
    } 
}
