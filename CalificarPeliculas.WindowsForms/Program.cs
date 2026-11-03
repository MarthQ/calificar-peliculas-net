using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Criticas;

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

            var apiBaseUrl = configuration["ApiBaseUrl"];
            if (string.IsNullOrWhiteSpace(apiBaseUrl))
            {
                MessageBox.Show("No se configuró ApiBaseUrl.", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            services.AddTransient<WinFormsTokenHandler>();
            services.AddHttpClient<ApiUsuarioServicio>(client => client.BaseAddress = new Uri(apiBaseUrl))
                .AddHttpMessageHandler<WinFormsTokenHandler>();
            services.AddHttpClient<ApiGeneroServicio>(client => client.BaseAddress = new Uri(apiBaseUrl))
                .AddHttpMessageHandler<WinFormsTokenHandler>();
            services.AddHttpClient<ApiContenidoServicio>(client => client.BaseAddress = new Uri(apiBaseUrl))
                .AddHttpMessageHandler<WinFormsTokenHandler>();
            services.AddHttpClient<ApiCriticaServicio>(client => client.BaseAddress = new Uri(apiBaseUrl))
                .AddHttpMessageHandler<WinFormsTokenHandler>();

            services.AddScoped<IUsuarioServicio>(sp => sp.GetRequiredService<ApiUsuarioServicio>());
            services.AddScoped<IGeneroServicio>(sp => sp.GetRequiredService<ApiGeneroServicio>());
            services.AddScoped<IContenidoServicio>(sp => sp.GetRequiredService<ApiContenidoServicio>());
            services.AddScoped<ICriticaServicio>(sp => sp.GetRequiredService<ApiCriticaServicio>());

            // Registramos formularios
            services.AddScoped<Form1>();
            services.AddScoped<LoginForm>();
            services.AddScoped<GeneroListForm>();
            services.AddScoped<GeneroEditForm>();
            services.AddScoped<ContenidoListForm>();
            services.AddScoped<ContenidoEditForm>();
            services.AddScoped<CriticaListForm>();
            services.AddScoped<CriticaEditForm>();
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
