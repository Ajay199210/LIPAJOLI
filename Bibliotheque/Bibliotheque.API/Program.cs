using Bibliotheque.ApplicationCore.Interfaces;
using Bibliotheque.ApplicationCore.Services;
using Bibliotheque.Infrastructure;
using Bibliotheque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Bibliotheque LIPAJOLI",
        Version = "v1",
        Description = "API d'exemple pour la gestion des emprunts de la bibliothèque LIPAJOLI dans le cadre du TP4",
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org")
        },
        Contact = new OpenApiContact
        {
            Name = "Groupe TP4",
            Email = "etudiants@cegeplimoilou.ca",
            Url = new Uri("https://cegeplimoilou.ca/")
        },
    });

    // Activation du support des commentaires XML dans Swagger UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<BibliothequeContext>(options =>
              options.UseLazyLoadingProxies()
              .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped<IUsagersService, UsagersService>();
builder.Services.AddScoped<ILivresService, LivresService>();
builder.Services.AddScoped<IEmpruntsService, EmpruntsService>();

JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
