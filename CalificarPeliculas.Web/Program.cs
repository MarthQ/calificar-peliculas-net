using CalificarPeliculas.Web;
using CalificarPeliculas.Repository;
using CalificarPeliculas.Application.Interfaces;
using CalificarPeliculas.Application.Services;
using CalificarPeliculas.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped<IContenidoRepository, ContenidoRepository>();
builder.Services.AddScoped<IContenidoServicio, ContenidoServicio>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IGeneroServicio, GeneroServicio>();

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

app.Run();
