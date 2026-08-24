using CalificarPeliculas.Domain.Genero;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CalificarPeliculas.Repository
{
    public class CFContext : DbContext
    {
        public DbSet<Genero> Generos { get; set; }

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
