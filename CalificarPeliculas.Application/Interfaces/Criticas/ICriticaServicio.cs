using CalificarPeliculas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Interfaces.Criticas
{
    public interface ICriticaServicio
    {
        Task<CriticaDTO> AddAsync(CriticaDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<CriticaDTO?> GetAsync(int id);
        Task<IEnumerable<CriticaDTO>> GetAllAsync();
        Task<bool> UpdateAsync(CriticaDTO dto);
    }
}
