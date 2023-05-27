using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace _3.Presentation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<BuscadorPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<NotificacionPage>();
            builder.Services.AddSingleton<PerfilPage>();


            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("_3.Presentation.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            App.Configuration = config;

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}