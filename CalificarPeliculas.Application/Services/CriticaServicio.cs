using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Services
{
    public class CriticaServicio : ICriticaServicio
    {
        private readonly ICriticaRepository criticaRepository;
        public CriticaServicio(ICriticaRepository criticaRepository)
        {
            this.criticaRepository = criticaRepository;
        }

        public async Task<CriticaDTO> AddAsync(CriticaDTO dto)
        {
            Critica critica = new Critica(0, dto.UsuarioId, dto.ContenidoId, dto.FechaPublicacion, dto.Texto, dto.Calificacion, dto.CantReacciones);
            await criticaRepository.AddAsync(critica);
            dto.Id = critica.Id;
            dto.UsuarioId = critica.UsuarioId;
            dto.ContenidoId = critica.ContenidoId;
            dto.FechaPublicacion = critica.FechaPublicacion;
            dto.Texto = critica.Texto;
            dto.Calificacion = critica.Calificacion;
            dto.CantReacciones = critica.CantReacciones;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await criticaRepository.DeleteAsync(id);
        }

        public async Task<CriticaDTO?> GetAsync(int id)
        {
            Critica? critica = await criticaRepository.GetAsync(id);

            if (critica == null)
                return null;

            return new CriticaDTO
            {
                Id = critica.Id,
                FechaPublicacion = critica.FechaPublicacion,
                Texto = critica.Texto,
                Calificacion = critica.Calificacion,
                CantReacciones = critica.CantReacciones,
                UsuarioId = critica.UsuarioId,
                ContenidoId = critica.ContenidoId

            };
        }

        public async Task<IEnumerable<CriticaDTO>> GetAllAsync()
        {
            var criticas = await criticaRepository.GetAllAsync();

            return criticas.Select(critica => new CriticaDTO
            {
                Id = critica.Id,
                FechaPublicacion = critica.FechaPublicacion,
                Texto = critica.Texto,
                Calificacion = critica.Calificacion,
                CantReacciones = critica.CantReacciones,
                UsuarioId = critica.UsuarioId,
                ContenidoId = critica.ContenidoId
            }).ToList();
        }

        public async Task<bool> UpdateAsync(CriticaDTO dto)
        {
            Critica critica = new Critica(dto.Id, dto.UsuarioId, dto.ContenidoId, dto.FechaPublicacion, dto.Texto, dto.Calificacion, dto.CantReacciones);
            return await criticaRepository.UpdateAsync(critica);
        }
    }
}
