using Biblioteca.Infraestrutura.Context;
using Biblioteca.Infraestrutura.Repositories;
using Biblioteca.Aplicacao.Interfaces;
using Biblioteca.Aplicacao.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaDB")));

builder.Services.AddScoped(typeof(Biblioteca.Dominio.Repositories.IGenericRepository<>), typeof(Biblioteca.Infraestrutura.Repositories.GenericRepository<>));
builder.Services.AddScoped<GenericRepository<Biblioteca.Dominio.Entities.Autor>>();
builder.Services.AddScoped<LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddSingleton<Biblioteca.Aplicacao.Interfaces.IMapper, Biblioteca.Aplicacao.Mappers.ServiceMapper>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetService<Microsoft.Extensions.Logging.ILogger<Program>>();
        logger?.LogError(ex, "Erro ao aplicar migrations na inicialização.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livro}/{action=Index}/{id?}");

app.Run();
