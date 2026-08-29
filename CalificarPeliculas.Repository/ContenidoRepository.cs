using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalificarPeliculas.Repository
{
    public class ContenidoRepository : IContenidoRepository
    {

        private CFContext CreateContext()
        {
            return new CFContext();
        }
        public async Task AddAsync(Contenido contenido)
        {
            using var context = CreateContext();
            context.Contenidos.Add(contenido);
            await context.SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var contenido = await context.Contenidos.FindAsync(id);
            if (contenido != null)
            {
                context.Contenidos.Remove(contenido);
                await context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<Contenido?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Contenidos.FindAsync(id);
        }

        public async Task<IEnumerable<Contenido>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Contenidos.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Contenido contenido)
        {
            using var context = CreateContext();
            var existing = await context.Contenidos.FirstOrDefaultAsync(g => g.Id == contenido.Id);
            if (existing != null)
            {
                existing.SetNombre(contenido.Nombre);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}