using Microsoft.EntityFrameworkCore;
using APIColoresDaltonicos.Repositories;
using APIColoresDaltonicos.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Metodos ya creados
builder.Services.AddControllers();
builder.Services.AddOpenApi();


// Metodos creados por mi
builder.Services.ConfigurarBaseDatos(builder.Configuration);
builder.Services.CofigurarDependencias();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Metodos creados por mi
app.AplicarMigraciones();

// Metodos ya creados
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
