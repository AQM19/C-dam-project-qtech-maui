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

            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("_3.Presentation.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            builder.Services.AddSingleton<IConfiguration>(config);

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}