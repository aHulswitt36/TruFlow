using Newtonsoft.Json;
using System.Reflection;
using Infrastructure;
using Infrastructure.Settings;
using MauiDemoPreview12.ViewModels;
//using MauiDemoPreview12.Pages;
using Microsoft.Maui.LifecycleEvents;
using MauiDemoPreview12.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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
                })
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android
                        .OnStart((activity) => AppCenter.Start("android={10c0932d-1e7f-4085-a020-ec34c69ef455};",typeof(Analytics),typeof(Crashes)))
                    );
#endif
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
            services.AddTransient<River>();
            services.AddTransient<RiverViewModel>();
            services.AddTransient<Rivers>();
            services.AddTransient<RiversViewModel>();
            //services.AddTransient(serviceType: typeof(Page), implementationType: typeof(CustomNavigationPage));

            services.AddSingleton<INavigationService, NavigationService>();

            builder.ConfigureLifecycleEvents(lifecycle => {
#if ANDROID
                lifecycle.AddAndroid(d => 
                    d.OnBackPressed(a => LogEvent("test"))
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
