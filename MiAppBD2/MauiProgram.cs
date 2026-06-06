using Microsoft.Extensions.Logging;

namespace MiAppBD2
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<Services.DatabaseService>();

            builder.Services.AddTransient<Services.TareaRepository>();
            builder.Services.AddTransient<Services.CategoriaRepository>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<TareaPage>();



            return builder.Build();
        }
    }
}
