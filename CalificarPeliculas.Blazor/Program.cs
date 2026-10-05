using CalificarPeliculas.Blazor.Components;
using CalificarPeliculas.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5126";

builder.Services.AddScoped<SesionState>();
builder.Services.AddTransient<TokenMessageHandler>();

builder.Services.AddHttpClient<AuthApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<GenerosApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<TokenMessageHandler>();
builder.Services.AddHttpClient<ContenidosApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<TokenMessageHandler>();
builder.Services.AddHttpClient<CriticasApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<TokenMessageHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
