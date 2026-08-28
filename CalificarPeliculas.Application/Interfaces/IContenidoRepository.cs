using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalificarPeliculas.Domain;

namespace CalificarPeliculas.Application.Interfaces
{
    public interface IContenidoRepository
    {
        Task AddAsync(Contenido contenido);
        Task<bool> DeleteAsync(int id);
        Task<Contenido?> GetAsync(int id);
        Task<IEnumerable<Contenido>> GetAllAsync();
        Task<bool> UpdateAsync(Contenido contenido);
    }
}
