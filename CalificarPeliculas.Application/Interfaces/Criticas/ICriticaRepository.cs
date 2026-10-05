using CalificarPeliculas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Application.Interfaces.Criticas;

    public interface ICriticaRepository
    {
        Task AddAsync(Critica critica);
        Task<bool> DeleteAsync(int id);
        Task<Critica?> GetAsync(int id);
        Task<IEnumerable<Critica>> GetAllAsync();
        Task<bool> UpdateAsync(Critica critica);
    }
