using _3.Presentation._2.View;
using _3.Presentation.View;
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
            builder.Services.AddSingleton<AddTareaPageView>();
            builder.Services.AddSingleton<BuscadorPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<EspeciesPageView>();
            builder.Services.AddSingleton<LecturasTerrarioPageView>();
            builder.Services.AddSingleton<NotificacionPage>();
            builder.Services.AddSingleton<ObservacionesPageView>();
            builder.Services.AddSingleton<PerfilPage>();
            builder.Services.AddSingleton<TareasPageView>();
            builder.Services.AddSingleton<TerrarioPageView>();
            builder.Services.AddSingleton<UserTerrarioPageView>();
            builder.Services.AddSingleton<UsuarioPageView>();

            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("_3.Presentation.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            App.Configuration = config;

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}