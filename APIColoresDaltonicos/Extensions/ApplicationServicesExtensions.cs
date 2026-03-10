using APIColoresDaltonicos.Repositories.Repositories.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Repositories.Repositories.Generic;
using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using APIColoresDaltonicos.Services.Encriptar;
using APIColoresDaltonicos.Services.Mappings;
using APIColoresDaltonicos.Services.Services.Generic;
using APIColoresDaltonicos.Services.Services.Usuarios;
using APIColoresDaltonicos.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Extensions
{
    // 1. CREAMOS NUESTRA PROPIA REGLA PARA SWAGGER (EL FILTRO)
    public class AuthHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var esAnonimo = context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

            if(esAnonimo)
                return;

            if (operation.Parameters == null)
                operation.Parameters = new List<IOpenApiParameter>();

            // Añadimos la cajita de "Authorization" a mano en cada ruta
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Escribe: Bearer {tu_token_aqui}",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                }
            });
        }
    }

    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection CofigurarDependencias(this IServiceCollection services)
        {

            // Registramos nuestros repositorios y servicios en el contenedor de dependencias
            // Repositorios
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IConfiguracionDaltonismoRepository, ConfiguracionDaltonismoRepository>();
            // Servicios
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Registramos el mapper
            services.AddAutoMapper(cfg => cfg.AddProfile<UsuarioProfile>());

            // Registramos los servicios de encriptación y token
            services.AddScoped<IEncriptacionService, EncriptacionService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection ConfigurarSeguridad(this IServiceCollection service, IConfiguration configuration)
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

        public static IServiceCollection AñadirSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Colores Daltonicos", Version = "v1" });
                c.OperationFilter<AuthHeaderFilter>();
            });

            return services;
        }
    }
}