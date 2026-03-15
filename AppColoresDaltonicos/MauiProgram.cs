using AppColoresDaltonicos.Extensions;
using AppColoresDaltonicos.Services;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CommunityToolkit.Maui;

namespace AppColoresDaltonicos
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.ConfigureApiServices().AddService();

            #if DEBUG
            builder.Logging.AddDebug();
            #endif

            var assembly = Assembly.GetExecutingAssembly();
            
            
            return builder.Build();
        }
    }
}
