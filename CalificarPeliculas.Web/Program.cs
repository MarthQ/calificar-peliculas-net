using CalificarPeliculas.Web;
using CalificarPeliculas.Repository;
using CalificarPeliculas.Application.Services;
using CalificarPeliculas.Application.DTOs;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Domain.Users;
using CalificarPeliculas.Repository.Users;
using CalificarPeliculas.Web.Security;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));

// Dependency injection
builder.Services.AddScoped<IContenidoRepository, ContenidoRepository>();
builder.Services.AddScoped<IContenidoServicio, ContenidoServicio>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IGeneroServicio, GeneroServicio>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IPasswordHashService, AspNetPasswordHashService>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

var app = builder.Build();

// Configure HTTP Request
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Map endpoints
app.MapGeneroEndpoints();
app.MapContenidoEndpoints();
app.MapUsuarioEndpoints();

app.Run();
