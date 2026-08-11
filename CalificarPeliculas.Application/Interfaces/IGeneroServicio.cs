using CalificarPeliculas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Interfaces
{
    public interface IGeneroServicio
    {
        Task<GeneroDTO> AddAsync(GeneroDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<GeneroDTO?> GetAsync(int id);
        Task<IEnumerable<GeneroDTO>> GetAllAsync();
        Task<bool> UpdateAsync(GeneroDTO dto);
    }
}
