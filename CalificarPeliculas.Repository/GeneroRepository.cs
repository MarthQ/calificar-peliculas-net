using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Domain;
using CalificarPeliculas.Domain.Genero;

namespace CalificarPeliculas.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private static readonly List<Genero> generos = new List<Genero>();
        private static int nuevoId = 1;
        public Task AddAsync(Genero genero)
        {
            genero.SetId(nuevoId);
            nuevoId++;
            generos.Add(genero);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var genero = generos.FirstOrDefault(c => c.Id == id);
            if (genero != null)
            {
                generos.Remove(genero);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Genero?> GetAsync(int id)
        {
            return Task.FromResult(generos.FirstOrDefault(c => c.Id == id));
        }

        public Task<IEnumerable<Genero>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Genero>>(generos.ToList());
        }

        public Task<bool> UpdateAsync(Genero genero)
        {
            var existing = generos.FirstOrDefault(c => c.Id == genero.Id);
            if (existing != null)
            {
                existing.SetNombre(genero.Nombre);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }


    }
}
