using Newtonsoft.Json;
using System.Reflection;
using Infrastructure;
using Infrastructure.Settings;
using MauiDemoPreview12.ViewModels;
//using MauiDemoPreview12.Pages;
using Microsoft.Maui.LifecycleEvents;
using MauiDemoPreview12.Services;

namespace MauiDemoPreview12
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
                    fonts.AddFont("MaterialIcons-Regular.ttf", "Material Icons");
                });
            var services = builder.Services;

            var assembly = Assembly.GetExecutingAssembly();
            var file = assembly.GetManifestResourceStream(
                assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("settings.json", System.StringComparison.OrdinalIgnoreCase))
            );
            using var sr = new StreamReader(file);
            var json = sr.ReadToEnd();
            var settings = JsonConvert.DeserializeObject<BaseSettings>(json);
            services.AddSingleton(settings.UsgsSettings);
            services.AddComponentLibrary("https://waterservices.usgs.gov/nwis/");
            services.AddTransient<MainPage>();
            services.AddTransient<Rivers>();
            services.AddTransient<RiversViewModel>();
            services.AddTransient<River>();
            services.AddTransient<RiverViewModel>();
            //services.AddTransient(serviceType: typeof(Page), implementationType: typeof(CustomNavigationPage));

            services.AddSingleton<INavigationService, NavigationService>();

            builder.ConfigureLifecycleEvents(lifecycle => {
#if ANDROID
                lifecycle.AddAndroid(d => 
                    d.OnBackPressed(a => LogEvent("fuck"))
                );
#endif
                static bool LogEvent(string eventName, string type = null)
                {
                    System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
                    return true;
                }
            });

            return builder.Build();
        }
    }
}
