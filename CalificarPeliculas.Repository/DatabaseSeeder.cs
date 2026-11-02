using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Domain;
using CalificarPeliculas.Domain.Users;

namespace CalificarPeliculas.Repository
{
    public static class DatabaseSeeder
    {
        public static void Seed(CFContext context, IPasswordHashService passwordHashService)
        {
            context.Database.EnsureCreated();

            var admin = context.Usuarios.FirstOrDefault(u => u.Mail == "admin@gmail.com");
            if (admin == null)
            {
                admin = new Usuario(0, "Administrador", "admin@gmail.com", (int)TipoRol.SuperUsuario);
                admin.SetPasswordHash(passwordHashService.HashPassword(admin, "Admin123!"));
                context.Usuarios.Add(admin);
                context.SaveChanges();
            }

            var generoAccion = context.Generos.FirstOrDefault(g => g.Nombre == "Acción");
            if (generoAccion == null)
            {
                generoAccion = new Genero(0, "Acción");
                context.Generos.Add(generoAccion);
            }

            var generoDrama = context.Generos.FirstOrDefault(g => g.Nombre == "Drama");
            if (generoDrama == null)
            {
                generoDrama = new Genero(0, "Drama");
                context.Generos.Add(generoDrama);
            }

            context.SaveChanges();

            var pelicula = context.Peliculas.FirstOrDefault(p => p.Nombre == "Peliculaza");
            if (pelicula == null)
            {
                pelicula = new Pelicula(
                    0,
                    1001,
                    "Peliculaza",
                    generoAccion.Id,
                    "Es una pelicula ciertamente",
                    "Yo",
                    new DateOnly(2024, 1, 15),
                    new TimeOnly(1, 45),
                    "https://placehold.co/600x900",
                    8.5f);
                context.Peliculas.Add(pelicula);
            }

            var serie = context.Series.FirstOrDefault(s => s.Nombre == "Una serie");
            if (serie == null)
            {
                serie = new Serie(
                    0,
                    1002,
                    "Una serie",
                    generoDrama.Id,
                    "Serie de ejemplo.",
                    "Yo tambien",
                    new DateOnly(2023, 6, 10),
                    new TimeOnly(0, 50),
                    "https://placehold.co/600x900",
                    9f,
                    2);
                context.Series.Add(serie);
            }

            context.SaveChanges();

            if (!context.Criticas.Any(c => c.UsuarioId == admin.Id && c.ContenidoId == pelicula.Id))
            {
                context.Criticas.Add(new Critica(
                    0,
                    admin.Id,
                    pelicula.Id,
                    new DateOnly(2024, 2, 1),
                    "La verdad no me gusto",
                    8.5f,
                    0));
                context.SaveChanges();
            }
        }
    }
}
