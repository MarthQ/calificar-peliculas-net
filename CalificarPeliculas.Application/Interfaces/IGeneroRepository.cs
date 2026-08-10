using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalificarPeliculas.Domain.Genero;

namespace CalificarPeliculas.Application.Interfaces
{
    public interface IGeneroRepository
    {
        Task AddAsync(Genero genero);
        Task<bool> DeleteAsync(int id);
        Task<Genero?> GetAsync(int id);
        Task<IEnumerable<Genero>> GetAllAsync();
        Task<bool> UpdateAsync(Genero genero);
    }
}
