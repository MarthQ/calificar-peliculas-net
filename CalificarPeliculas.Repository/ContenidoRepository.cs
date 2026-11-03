using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Domain;
using CalificarPeliculas.Application.Interfaces.Contenido;
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

        public async Task<Contenido?> GetWithCriticasAsync(int id)
        {
            using var context = CreateContext();
            return await context.Contenidos
                .Include(c => c.Criticas)
                .FirstOrDefaultAsync(c => c.Id == id);
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
                existing.SetIdTMDB(contenido.IdTMDB);
                existing.SetNombre(contenido.Nombre);
                existing.SetGeneroId(contenido.GeneroId);
                existing.SetDescripcion(contenido.Descripcion);
                existing.SetNombreDirector(contenido.NombreDirector);
                existing.SetFechaLanzamiento(contenido.FechaLanzamiento);
                existing.SetDuracion(contenido.Duracion);
                existing.SetUrlImagen(contenido.UrlImagen);
                existing.SetPuntuacionPromedio(contenido.PuntuacionPromedio);

                if (existing is Episodio existingEpisodio && contenido is Episodio episodio)
                {
                    existingEpisodio.SetNumEp(episodio.NumeroEpisodio);
                    existingEpisodio.SetNumTemp(episodio.NumeroTemporada);
                }
                else if (existing is Serie existingSerie && contenido is Serie serie)
                {
                    existingSerie.SetCantTemporadas(serie.CantTemporadas);
                }

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
