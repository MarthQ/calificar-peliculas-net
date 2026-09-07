using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CalificarPeliculas.Repository;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Application.Services;
using CalificarPeliculas.Repository.Users;
using CalificarPeliculas.Web.Security;
using CalificarPeliculas.Domain.Users;
using Microsoft.AspNetCore.Identity;
using CalificarPeliculas.Application.Interfaces.Contenido;

namespace CalificarPeliculas.WindowsForms
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Configuración del Forms
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            // Registramos la conexión a la DB
            var conn = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrWhiteSpace(conn))
            {
                services.AddDbContext<CFContext>(options => options.UseSqlServer(conn));
            }


            // Registramos servicios y repositorios
            services.AddScoped<IUsuarioServicio, UsuarioServicio>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPasswordHashService, AspNetPasswordHashService>();
            
            // Contenido y Genero
            services.AddScoped<IGeneroServicio, GeneroServicio>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IContenidoServicio, ContenidoServicio>();
            services.AddScoped<IContenidoRepository, ContenidoRepository>();

            // Registro del hasher de Identity que usa AspNetPasswordHashService
            services.AddSingleton<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

            // Registramos formularios
            services.AddScoped<Form1>();
            services.AddScoped<LoginForm>();
            services.AddScoped<GeneroListForm>();
            services.AddScoped<GeneroEditForm>();
            services.AddScoped<ContenidoListForm>();
            services.AddScoped<ContenidoEditForm>();
            services.AddScoped<RegisterForm>();

            ServiceProvider = services.BuildServiceProvider();

            // Crear alcance para resolver servicios
            using var scope = ServiceProvider.CreateScope();
            var provider = scope.ServiceProvider;

            // Intentar resolver IUsuarioServicio
            var usuarioServicio = provider.GetService<IUsuarioServicio>();

            // Crear LoginForm manualmente para poder manejar el caso en que usuarioServicio sea null
            using var login = new LoginForm(usuarioServicio);
            var dr = login.ShowDialog();

            if (dr == DialogResult.OK)
            {
                // Solo abro el formulario principal si el login devolvió OK (y el usuario es admin según LoginForm)
                var main = provider.GetRequiredService<Form1>();
                System.Windows.Forms.Application.Run(main);
            }
        }
    }
}
