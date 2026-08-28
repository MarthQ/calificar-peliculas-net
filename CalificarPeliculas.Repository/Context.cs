using CalificarPeliculas.Domain;
using Microsoft.EntityFrameworkCore;
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
                entity.HasDiscriminator(ent => ent.Tipo)
                .HasValue<Episodio>("EPISODIO")
                .HasValue<Pelicula>("PELICULA")
                .HasValue<Serie>("SERIE");

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
        }
    }
}
