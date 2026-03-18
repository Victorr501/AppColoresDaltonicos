using AppColoresDaltonicos.Services.Api;
using AppColoresDaltonicos.Services.Auth;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AppColoresDaltonicos.Extensions
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder ConfigureApiServices(this MauiAppBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("AppColoresDaltonicos.Properties.appsettings.json");

            if (stream != null)
            { 
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                var baseUrl = builder.Configuration["ApiConfig:BaseUrl"];
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    client.BaseAddress = new Uri(baseUrl);
                }
            });

            return builder;
        }

        public static MauiAppBuilder AddService(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IAuthService, AuthService>();

            return builder;
        }
    }
}
