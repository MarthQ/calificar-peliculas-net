using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Domain;
using Microsoft.EntityFrameworkCore;

namespace CalificarPeliculas.Repository
{
    public class GeneroRepository : IGeneroRepository
    {

        private CFContext CreateContext()
        {
            return new CFContext();
        }
        public async Task AddAsync(Genero genero)
        {
            using var context = CreateContext();
            context.Generos.Add(genero);
            await context.SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var genero = await context.Generos.FindAsync(id);
            if (genero != null)
            {
                context.Generos.Remove(genero);
                await context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<Genero?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Generos.FindAsync(id);
        }

        public async Task<IEnumerable<Genero>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Generos.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Genero genero)
        {
            using var context = CreateContext();
            var existing = await context.Generos.FirstOrDefaultAsync(g => g.Id == genero.Id);
            if (existing != null)
            {
                existing.SetNombre(genero.Nombre);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
