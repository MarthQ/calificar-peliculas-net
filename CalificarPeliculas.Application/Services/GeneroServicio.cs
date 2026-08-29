using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Services
{
    public class GeneroServicio : IGeneroServicio
    {
        private readonly IGeneroRepository generoRepository;
        public GeneroServicio(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }

        public async Task<GeneroDTO> AddAsync(GeneroDTO dto)
        {
            Genero genero = new Genero(0, dto.Nombre);
            await generoRepository.AddAsync(genero);
            dto.Id = genero.Id;
            dto.Nombre = genero.Nombre;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await generoRepository.DeleteAsync(id);
        }

        public async Task<GeneroDTO?> GetAsync(int id)
        {
            Genero? genero = await generoRepository.GetAsync(id);

            if (genero == null)
                return null;

            return new GeneroDTO
            {
                Id = genero.Id,
                Nombre = genero.Nombre
            };
        }

        public async Task<IEnumerable<GeneroDTO>> GetAllAsync()
        {
            var generos = await generoRepository.GetAllAsync();

            return generos.Select(genero => new GeneroDTO
            {
                Id = genero.Id,
                Nombre = genero.Nombre,
            }).ToList();
        }

        public async Task<bool> UpdateAsync(GeneroDTO dto)
        {
            Genero genero = new Genero(dto.Id, dto.Nombre);
            return await generoRepository.UpdateAsync(genero);
        }

    }
}
