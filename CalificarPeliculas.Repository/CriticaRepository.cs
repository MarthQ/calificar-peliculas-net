using CalificarPeliculas.Domain;
using CalificarPeliculas.Application.Interfaces.Criticas;
using Microsoft.EntityFrameworkCore;

namespace CalificarPeliculas.Repository
{
    public class CriticaRepository : ICriticaRepository
    {

        private CFContext CreateContext()
        {
            return new CFContext();
        }
        public async Task AddAsync(Critica critica)
        {
            using var context = CreateContext();
            context.Criticas.Add(critica);
            await context.SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var critica = await context.Criticas.FindAsync(id);
            if (critica != null)
            {
                context.Criticas.Remove(critica);
                await context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<Critica?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Criticas.FindAsync(id);
        }

        public async Task<IEnumerable<Critica>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Criticas.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Critica critica)
        {
            using var context = CreateContext();
            var existing = await context.Criticas.FirstOrDefaultAsync(c => c.Id == critica.Id);
            if (existing != null)
            {
                existing.SetTexto(critica.Texto);
                existing.SetFecha(critica.FechaPublicacion);
                existing.SetCalificacion(critica.Calificacion);
                existing.SetCantReacciones(critica.CantReacciones);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
