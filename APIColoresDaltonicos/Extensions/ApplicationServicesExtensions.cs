using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using APIColoresDaltonicos.Services.Mappings;
using APIColoresDaltonicos.Services.Services.Generic;


namespace APIColoresDaltonicos.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection CofigurarDependencias(this IServiceCollection services) 
        {
            // Aquí se añadiran los repositorios
            // Añadimos el generico
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            // Aquí se añadiran los servicios
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Aqui es donde se añade el mapper
            services.AddAutoMapper(cfg => cfg.AddProfile<UsuarioProfile>());

            return services;
        }
    }
}
