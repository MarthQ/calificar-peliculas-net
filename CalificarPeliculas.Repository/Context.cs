using CalificarPeliculas.Domain;
using Microsoft.EntityFrameworkCore;
using CalificarPeliculas.Domain.Users;
using Microsoft.Extensions.Configuration;

namespace CalificarPeliculas.Repository
{
    public class CFContext : DbContext
    {
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Contenido> Contenidos { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public CFContext(DbContextOptions<DbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        internal CFContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contenido>(entity =>
            {
                entity.HasKey(ent => ent.Id);
                entity.Property(ent => ent.Id)
                    .ValueGeneratedOnAdd();
               
                entity.UseTphMappingStrategy();
                // El enum se conserva como texto para mantener compatible el discriminador existente y legible en SQL.
                entity.Property(ent => ent.Tipo)
                    .HasConversion<string>();
                entity.HasDiscriminator(ent => ent.Tipo)
                .HasValue<Episodio>(TipoContenido.EPISODIO)
                .HasValue<Pelicula>(TipoContenido.PELICULA)
                .HasValue<Serie>(TipoContenido.SERIE);

                // entity.Property(ent => ent.IdTMDB)
                // .IsRequired();
                // (A futuro, cuando implementemos TMDB)

                entity.Property(ent => ent.GeneroId)
                .IsRequired()
                .HasField("_generoId");

                entity.Navigation(ent => ent.Genero)
                .HasField("_genero");
                
                entity.HasOne(ent => ent.Genero)
                    .WithMany()
                    .HasForeignKey(ent => ent.GeneroId);

                entity.Property(ent => ent.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(ent => ent.Descripcion)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(ent => ent.NombreDirector)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(ent => ent.FechaLanzamiento)
                    .IsRequired();

                entity.Property(ent => ent.Duracion)
                    .IsRequired();

                entity.Property(ent => ent.UrlImagen)
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(ent => ent.Id);
                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();
                entity.Property(ent => ent.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(ent => ent.Id);
                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.Property(ent => ent.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasIndex(ent => ent.NombreUsuario).IsUnique();

                entity.Property(ent => ent.Mail)
                    .IsRequired()
                    .HasMaxLength(256);
                entity.HasIndex(ent => ent.Mail).IsUnique();

                entity.Property(ent => ent.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(ent => ent.RolId)
                    .IsRequired()
                    .HasField("_rolId");

                entity.Navigation(ent => ent.Rol)
                    .HasField("_rol");

                entity.HasOne(ent => ent.Rol)
                    .WithMany()
                    .HasForeignKey(ent => ent.RolId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(ent => ent.Id);
                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.Property(ent => ent.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(ent => ent.Denominacion)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasIndex(ent => ent.Denominacion).IsUnique();

                entity.HasData(
                    new { Id = 1, Description = "Superusuario", Denominacion = TipoRol.SuperUsuario },
                    new { Id = 2, Description = "Moderador", Denominacion = TipoRol.Moderador },
                    new { Id = 3, Description = "Usuario", Denominacion = TipoRol.Usuario });
            });
        }
    }
}
