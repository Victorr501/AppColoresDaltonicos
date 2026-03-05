using Microsoft.EntityFrameworkCore;
using APIColoresDaltonicos.Repositories;

namespace APIColoresDaltonicos.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AplicarMigraciones(this WebApplication app)
        {
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
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocurrió un error al migrar la base de datos.");
                }
            }
        }

        public static IServiceCollection ConfigurarBaseDatos(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            return services;
        }
    }
}
