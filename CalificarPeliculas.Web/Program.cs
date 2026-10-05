using CalificarPeliculas.Web;
using CalificarPeliculas.Repository;
using CalificarPeliculas.Application.Services;
using CalificarPeliculas.Application.Interfaces.Contenido;
using CalificarPeliculas.Application.Interfaces.Usuario;
using CalificarPeliculas.Application.Interfaces.Criticas;
using CalificarPeliculas.Domain.Users;
using CalificarPeliculas.Repository.Users;
using CalificarPeliculas.Web.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));

// Autenticacion JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

// CORS para el cliente Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Dependency injection
builder.Services.AddScoped<IContenidoRepository, ContenidoRepository>();
builder.Services.AddScoped<IContenidoServicio, ContenidoServicio>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IGeneroServicio, GeneroServicio>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICriticaRepository, CriticaRepository>();
builder.Services.AddScoped<ICriticaServicio, CriticaServicio>();
builder.Services.AddScoped<IPasswordHashService, AspNetPasswordHashService>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

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

app.UseCors("BlazorClient");
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapGeneroEndpoints();
app.MapContenidoEndpoints();
app.MapUsuarioEndpoints();
app.MapCriticaEndpoints();

app.Run();
