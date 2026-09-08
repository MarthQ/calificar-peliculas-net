using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContenidoEntidad = CalificarPeliculas.Domain.Contenido;

namespace CalificarPeliculas.Application.Interfaces.Contenido
{
    public interface IContenidoRepository
    {
        Task AddAsync(ContenidoEntidad contenido);
        Task<bool> DeleteAsync(int id);
        Task<ContenidoEntidad?> GetAsync(int id);
        Task<IEnumerable<ContenidoEntidad>> GetAllAsync();
        Task<bool> UpdateAsync(ContenidoEntidad contenido);
    }
}
