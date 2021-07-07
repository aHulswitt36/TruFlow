using Infrastructure;
using MauiDemo.Pages;
using MauiDemo.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MauiDemo
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .UseMauiApp<App>()
                .UseMicrosoftExtensionsServiceProviderFactory()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var file = assembly.GetManifestResourceStream(
                        assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("settings.json", System.StringComparison.OrdinalIgnoreCase))
                    );
                    using var sr = new StreamReader(file);
                    var json = sr.ReadToEnd();
                    var settings = JsonConvert.DeserializeObject<Infrastructure.Settings.BaseSettings>(json);
                    services.AddSingleton(settings.UsgsSettings);
                    services.AddComponentLibrary("https://waterservices.usgs.gov/nwis/");
                    services.AddTransient<RiversViewModel>();
                    services.AddTransient(serviceType: typeof(Page),
                        implementationType: typeof(CustomNavigationPage));
                }).ConfigureLifecycleEvents(lifecycle => {
#if ANDROID
					lifecycle.AddAndroid(d => {
						d.OnBackPressed(activity => {
							System.Diagnostics.Debug.WriteLine("Back button pressed!");
						});
					});
#endif
                });
        }
    }
}