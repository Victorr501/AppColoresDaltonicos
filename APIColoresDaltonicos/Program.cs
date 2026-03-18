using Microsoft.EntityFrameworkCore;
using APIColoresDaltonicos.Repositories;
using APIColoresDaltonicos.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Metodos ya creados
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Metodos creados por mi
builder.Services.ConfigurarBaseDatos(builder.Configuration);
builder.Services.CofigurarDependencias();
builder.Services.ConfigurarSeguridad(builder.Configuration);
builder.Services.AñadirSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Metodos creados por mi
app.AplicarMigraciones();

// Metodos ya creados
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
