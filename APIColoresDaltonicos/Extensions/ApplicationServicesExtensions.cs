using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using Microsoft.IdentityModel.Tokens;
using APIColoresDaltonicos.Services.Mappings;
using APIColoresDaltonicos.Services.Services.Generic;
using APIColoresDaltonicos.Services.Services.Usuarios;
using APIColoresDaltonicos.Services.Encriptar;
using APIColoresDaltonicos.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


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

            // Aquí se añadiran los servicios de autenticación y autorización
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection ConfigurarSeguridad(this IServiceCollection service,IConfiguration configuration)
        {

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                    };
                });

            return service;
        }
    }
}
