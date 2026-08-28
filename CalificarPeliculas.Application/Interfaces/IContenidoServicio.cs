using CalificarPeliculas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Interfaces
{
    public interface IContenidoServicio
    {
        Task<ContenidoDTO> AddAsync(ContenidoDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<ContenidoDTO?> GetAsync(int id);
        Task<IEnumerable<ContenidoDTO>> GetAllAsync();
        Task<bool> UpdateAsync(ContenidoDTO dto);
    }
}
