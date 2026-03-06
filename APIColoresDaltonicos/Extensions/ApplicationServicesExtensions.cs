using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using APIColoresDaltonicos.Services.Mappings;
using APIColoresDaltonicos.Services.Services.Generic;
using APIColoresDaltonicos.Services.Services.Usuarios;
using APIColoresDaltonicos.Services.Encriptar;


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
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Aqui es donde se añade el mapper
            services.AddAutoMapper(cfg => cfg.AddProfile<UsuarioProfile>());

            // Aquí se añadiran los servicios de ecriptar
            services.AddScoped<IEncriptacionService, EncriptacionService>();

            return services;
        }
    }
}
