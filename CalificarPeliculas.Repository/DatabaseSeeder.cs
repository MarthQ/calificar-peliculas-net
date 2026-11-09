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

            var cinefilos = GetOrAddGrupo(context, "Cinéfilos", "Usuarios interesados en compartir opiniones sobre películas y series.", new DateOnly(2024, 1, 10));
            var fansSeries = GetOrAddGrupo(context, "Fans de series", "Seguidores de series que comentan temporadas y episodios.", new DateOnly(2024, 1, 15));
            context.SaveChanges();

            var admin = GetOrAddUsuario(context, passwordHashService, "Administrador", "admin@gmail.com", "Admin123!", TipoRol.SuperUsuario, cinefilos.Id);
            var moderador = GetOrAddUsuario(context, passwordHashService, "Moderador", "moderador@gmail.com", "Moderador123!", TipoRol.Moderador, cinefilos.Id);
            var lucia = GetOrAddUsuario(context, passwordHashService, "lucia_cine", "lucia@gmail.com", "Usuario123!", TipoRol.Usuario, cinefilos.Id);
            var tomas = GetOrAddUsuario(context, passwordHashService, "tomas_series", "tomas@gmail.com", "Usuario123!", TipoRol.Usuario, fansSeries.Id);
            var valentina = GetOrAddUsuario(context, passwordHashService, "valen_m", "valentina@gmail.com", "Usuario123!", TipoRol.Usuario, null);
            context.SaveChanges();

            var generos = new[] { "Acción", "Drama", "Comedia", "Ciencia ficción", "Thriller", "Animación" }
                .ToDictionary(nombre => nombre, nombre => GetOrAddGenero(context, nombre));
            context.SaveChanges();

            var c = new Dictionary<string, Contenido>();
            void AgregarPelicula(string nombre, string genero, string descripcion, string director, DateOnly fecha, TimeOnly duracion) =>
                c[nombre] = GetOrAddContenido(context, nombre, () => new Pelicula(0, 0, nombre, generos[genero].Id, descripcion, director, fecha, duracion, Imagen(nombre), 0));
            void AgregarSerie(string nombre, string genero, string descripcion, string director, DateOnly fecha, TimeOnly duracion, int temporadas) =>
                c[nombre] = GetOrAddContenido(context, nombre, () => new Serie(0, 0, nombre, generos[genero].Id, descripcion, director, fecha, duracion, Imagen(nombre), 0, temporadas));

            AgregarPelicula("El secreto de sus ojos", "Drama", "Un empleado judicial retirado escribe una novela sobre un caso de asesinato que nunca pudo cerrar.", "Juan José Campanella", new DateOnly(2009, 8, 13), new TimeOnly(2, 9));
            AgregarPelicula("Relatos salvajes", "Comedia", "Seis historias independientes sobre personas que pierden el control ante situaciones límite.", "Damián Szifron", new DateOnly(2014, 8, 21), new TimeOnly(2, 2));
            AgregarPelicula("Nueve reinas", "Thriller", "Dos estafadores se cruzan en Buenos Aires y planean vender una falsificación de estampillas valiosas.", "Fabián Bielinsky", new DateOnly(2000, 8, 31), new TimeOnly(1, 54));
            AgregarPelicula("El Padrino", "Drama", "El patriarca de una familia mafiosa transfiere el control de su imperio a su hijo menor.", "Francis Ford Coppola", new DateOnly(1972, 3, 24), new TimeOnly(2, 55));
            AgregarPelicula("Matrix", "Ciencia ficción", "Un programador descubre que la realidad es una simulación creada por máquinas.", "Lana y Lilly Wachowski", new DateOnly(1999, 3, 31), new TimeOnly(2, 16));
            AgregarPelicula("Parásitos", "Thriller", "Una familia pobre se infiltra, uno a uno, en la casa de una familia adinerada.", "Bong Joon-ho", new DateOnly(2019, 5, 30), new TimeOnly(2, 12));
            AgregarPelicula("Mad Max: Furia en el camino", "Acción", "En un desierto postapocalíptico, una guerrera huye de un tirano junto a un grupo de prisioneras.", "George Miller", new DateOnly(2015, 5, 15), new TimeOnly(2, 0));
            AgregarPelicula("Toy Story", "Animación", "Los juguetes de un niño cobran vida cuando nadie los ve, y un nuevo muñeco amenaza el lugar del favorito.", "John Lasseter", new DateOnly(1995, 11, 22), new TimeOnly(1, 21));
            AgregarSerie("Breaking Bad", "Drama", "Un profesor de química comienza a fabricar metanfetamina para asegurar el futuro de su familia.", "Vince Gilligan", new DateOnly(2008, 1, 20), new TimeOnly(0, 47), 5);
            AgregarSerie("The Office", "Comedia", "Un falso documental sobre la vida diaria en la oficina de una distribuidora de papel.", "Greg Daniels", new DateOnly(2005, 3, 24), new TimeOnly(0, 22), 9);
            AgregarSerie("Dark", "Ciencia ficción", "La desaparición de dos chicos en un pueblo alemán revela un secreto que conecta a cuatro familias a lo largo del tiempo.", "Baran bo Odar", new DateOnly(2017, 12, 1), new TimeOnly(0, 55), 3);
            AgregarSerie("Stranger Things", "Ciencia ficción", "Un grupo de amigos busca a un chico desaparecido y se enfrenta a fuerzas sobrenaturales.", "Matt y Ross Duffer", new DateOnly(2016, 7, 15), new TimeOnly(0, 50), 5);
            c["Ozymandias"] = GetOrAddContenido(context, "Ozymandias", () => new Episodio(0, 0, "Ozymandias", generos["Drama"].Id, "Episodio 14 de la quinta temporada de Breaking Bad.", "Rian Johnson", new DateOnly(2013, 9, 15), new TimeOnly(0, 47), Imagen("Ozymandias"), 0, 14, 5));
            context.SaveChanges();

            if (!context.Criticas.Any())
            {
                var criticas = new List<Critica>
                {
                    new(0, lucia.Id, c["El secreto de sus ojos"].Id, new DateOnly(2024, 3, 2), "Una historia de amor y justicia con una de las mejores escenas en un estadio del cine.", 9.5f, 14),
                    new(0, tomas.Id, c["El secreto de sus ojos"].Id, new DateOnly(2024, 4, 18), "Muy buena, aunque el ritmo del principio se hace un poco lento.", 8f, 3),
                    new(0, valentina.Id, c["Relatos salvajes"].Id, new DateOnly(2024, 2, 11), "Cada historia funciona por separado. La del casamiento es lo mejor.", 8.5f, 9),
                    new(0, lucia.Id, c["Nueve reinas"].Id, new DateOnly(2024, 5, 6), "Un guion impecable, te engaña hasta el último minuto.", 9f, 11),
                    new(0, moderador.Id, c["Nueve reinas"].Id, new DateOnly(2024, 6, 1), "Clásico del cine argentino. Darín y Pauls están perfectos.", 8.5f, 4),
                    new(0, tomas.Id, c["El Padrino"].Id, new DateOnly(2024, 1, 29), "Larga pero no sobra nada. La actuación de Brando es enorme.", 10f, 20),
                    new(0, valentina.Id, c["Matrix"].Id, new DateOnly(2024, 3, 22), "Los efectos envejecieron bien y la idea sigue siendo potente.", 8.5f, 7),
                    new(0, lucia.Id, c["Matrix"].Id, new DateOnly(2024, 7, 9), "Entretenida, aunque la segunda mitad abusa de las peleas.", 7f, 2),
                    new(0, admin.Id, c["Parásitos"].Id, new DateOnly(2024, 2, 3), "Empieza como comedia y termina como thriller sin perder coherencia.", 9f, 8),
                    new(0, tomas.Id, c["Mad Max: Furia en el camino"].Id, new DateOnly(2024, 8, 14), "Dos horas de persecución que nunca cansan.", 8f, 5),
                    new(0, valentina.Id, c["Toy Story"].Id, new DateOnly(2024, 9, 1), "La vi de chica y de adulta, y funciona igual de bien.", 9f, 6),
                    new(0, tomas.Id, c["Breaking Bad"].Id, new DateOnly(2024, 4, 2), "La mejor evolución de un personaje que vi en una serie.", 10f, 25),
                    new(0, lucia.Id, c["Breaking Bad"].Id, new DateOnly(2024, 5, 30), "Las primeras temporadas tardan en arrancar, después no se puede parar.", 9f, 6),
                    new(0, valentina.Id, c["The Office"].Id, new DateOnly(2024, 6, 12), "Muy graciosa, aunque las últimas temporadas bajan mucho.", 7.5f, 4),
                    new(0, tomas.Id, c["Dark"].Id, new DateOnly(2024, 10, 5), "Hay que verla con un cuaderno al lado, pero cierra todo.", 9f, 10),
                    new(0, lucia.Id, c["Stranger Things"].Id, new DateOnly(2024, 11, 20), "La primera temporada es excelente; después se repite un poco.", 7f, 3),
                    new(0, tomas.Id, c["Ozymandias"].Id, new DateOnly(2024, 4, 20), "Probablemente el mejor episodio de la serie.", 10f, 18),
                };
                context.Criticas.AddRange(criticas);
                context.SaveChanges();

                context.Comentarios.AddRange(
                    new Comentario(0, criticas[0].Id, tomas.Id, "Totalmente de acuerdo con lo del estadio.", new DateOnly(2024, 3, 5), "Me gusta"),
                    new Comentario(0, criticas[0].Id, valentina.Id, "A mí el final me pareció un poco forzado.", new DateOnly(2024, 3, 8), "No coincido"),
                    new Comentario(0, criticas[3].Id, moderador.Id, "Y el final es de los mejores del cine nacional.", new DateOnly(2024, 5, 10), "Me gusta"),
                    new Comentario(0, criticas[5].Id, lucia.Id, "La segunda parte está a la misma altura.", new DateOnly(2024, 2, 2), "Recomendado"),
                    new Comentario(0, criticas[7].Id, valentina.Id, "Para mí las peleas son lo mejor.", new DateOnly(2024, 7, 12), "No coincido"),
                    new Comentario(0, criticas[11].Id, lucia.Id, "Coincido, el cambio de Walter es impresionante.", new DateOnly(2024, 4, 4), "Me gusta"),
                    new Comentario(0, criticas[11].Id, valentina.Id, "Tengo que terminarla, me quedé en la tercera.", new DateOnly(2024, 4, 10), "Pendiente"),
                    new Comentario(0, criticas[14].Id, lucia.Id, "El cuaderno es casi obligatorio.", new DateOnly(2024, 10, 7), "Me gusta"),
                    new Comentario(0, criticas[16].Id, admin.Id, "Imposible no estar de acuerdo.", new DateOnly(2024, 4, 21), "Recomendado"));
                context.SaveChanges();

                foreach (var contenido in c.Values)
                {
                    var calificaciones = criticas.Where(x => x.ContenidoId == contenido.Id).Select(x => x.Calificacion).ToList();
                    if (calificaciones.Count > 0)
                        contenido.SetPuntuacionPromedio((float)Math.Round(calificaciones.Average(), 1));
                }
                context.SaveChanges();
            }
        }

        private static string Imagen(string nombre) => $"https://placehold.co/600x900?text={Uri.EscapeDataString(nombre)}";

        private static Grupo GetOrAddGrupo(CFContext context, string nombre, string descripcion, DateOnly fecha)
        {
            var grupo = context.Grupos.FirstOrDefault(g => g.Nombre == nombre);
            if (grupo != null) return grupo;
            grupo = new Grupo(0, nombre, descripcion, fecha);
            context.Grupos.Add(grupo);
            return grupo;
        }

        private static Usuario GetOrAddUsuario(CFContext context, IPasswordHashService hash, string nombre, string mail, string password, TipoRol rol, int? grupoId)
        {
            var usuario = context.Usuarios.FirstOrDefault(u => u.Mail == mail);
            if (usuario != null) return usuario;
            usuario = new Usuario(0, nombre, mail, (int)rol);
            usuario.SetGrupoId(grupoId);
            usuario.SetPasswordHash(hash.HashPassword(usuario, password));
            context.Usuarios.Add(usuario);
            return usuario;
        }

        private static Genero GetOrAddGenero(CFContext context, string nombre)
        {
            var genero = context.Generos.FirstOrDefault(g => g.Nombre == nombre);
            if (genero != null) return genero;
            genero = new Genero(0, nombre);
            context.Generos.Add(genero);
            return genero;
        }

        private static Contenido GetOrAddContenido(CFContext context, string nombre, Func<Contenido> crear)
        {
            var contenido = context.Contenidos.FirstOrDefault(x => x.Nombre == nombre);
            if (contenido != null) return contenido;
            contenido = crear();
            context.Contenidos.Add(contenido);
            return contenido;
        }
    }
}
