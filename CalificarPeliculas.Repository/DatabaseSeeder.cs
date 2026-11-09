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

            var grupoCinefilos = context.Grupos.FirstOrDefault(g => g.Nombre == "Cinéfilos");
            if (grupoCinefilos == null)
            {
                grupoCinefilos = new Grupo(0, "Cinéfilos", "Usuarios interesados en compartir opiniones sobre películas y series.", new DateOnly(2024, 1, 10));
                context.Grupos.Add(grupoCinefilos);
            }

            var grupoSeries = context.Grupos.FirstOrDefault(g => g.Nombre == "Fans de series");
            if (grupoSeries == null)
            {
                grupoSeries = new Grupo(0, "Fans de series", "Grupo de ejemplo para seguidores de series.", new DateOnly(2024, 1, 15));
                context.Grupos.Add(grupoSeries);
            }

            context.SaveChanges();
            if (admin.GrupoId == null)
            {
                admin.SetGrupoId(grupoCinefilos.Id);
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
                    "Es una película ciertamente",
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
                    "La verdad no me gustó",
                    8.5f,
                    0));
                context.SaveChanges();
            }

            var criticaEjemplo = context.Criticas.FirstOrDefault(c => c.UsuarioId == admin.Id && c.ContenidoId == pelicula.Id);
            if (criticaEjemplo != null)
            {
                if (!context.Comentarios.Any(c => c.CriticaId == criticaEjemplo.Id && c.Texto == "Coincido con la calificación."))
                {
                    context.Comentarios.Add(new Comentario(
                        0,
                        criticaEjemplo.Id,
                        admin.Id,
                        "Coincido con la calificación.",
                        new DateOnly(2024, 2, 2),
                        "Me gusta"));
                }

                if (!context.Comentarios.Any(c => c.CriticaId == criticaEjemplo.Id && c.Texto == "La volvería a ver."))
                {
                    context.Comentarios.Add(new Comentario(
                        0,
                        criticaEjemplo.Id,
                        admin.Id,
                        "La volvería a ver.",
                        new DateOnly(2024, 2, 3),
                        "Recomendado"));
                }

                context.SaveChanges();
            }
        }
    }
}
