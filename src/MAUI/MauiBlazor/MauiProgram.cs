using Infrastructure;
using Infrastructure.Settings;
using MauiBlazor.Helpers;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Newtonsoft.Json;
using System.Reflection;

namespace MauiBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .RegisterBlazorMauiWebView()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureMauiHandlers(h =>
                {
#if ANDROID
                    h.AddHandler<IBlazorWebView, MauiBlazorWebViewHandler>();
#endif
                })
                .ConfigureEssentials(essentials =>
                {
                    essentials.AddAppAction("app_info", "App Info", icon: "app_info_action_icon");
                    essentials.AddAppAction("battery_info", "Battery Info");
                    essentials.OnAppAction(App.HandleAppActions);
                });

            builder.Services.AddBlazorWebView();
            var assembly = Assembly.GetExecutingAssembly();
            var file = assembly.GetManifestResourceStream(
                assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("settings.json", System.StringComparison.OrdinalIgnoreCase))
            );
            using var sr = new StreamReader(file);
            var json = sr.ReadToEnd();
            var settings = JsonConvert.DeserializeObject<BaseSettings>(json);
            builder.Services.AddSingleton(settings.UsgsSettings);
            builder.Services.AddComponentLibrary("https://waterservices.usgs.gov/nwis/");

            return builder.Build();
        }
    }
}
