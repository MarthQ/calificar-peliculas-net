using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Domain;
using CalificarPeliculas.Application.Interfaces.Contenido;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
            var connectionString = context.Database.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("No se configuró la cadena de conexión de la base de datos.");
            }

            var generos = new List<Genero>();
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand(
                "SELECT Id, Nombre FROM Generos ORDER BY Nombre",
                connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                generos.Add(new Genero(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetString(reader.GetOrdinal("Nombre"))));
            }

            return generos;
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
