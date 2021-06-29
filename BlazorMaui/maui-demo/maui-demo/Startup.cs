using Infrastructure;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace maui_demo
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
                    var resName = assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("settings.json", System.StringComparison.OrdinalIgnoreCase));
                    var file = assembly.GetManifestResourceStream(resName);
                    using var sr = new StreamReader(file);
                    var json = sr.ReadToEnd();
                    var settings = JsonConvert.DeserializeObject<BaseSettings>(json);
                    services.AddSingleton(settings.UsgsSettings);
                    services.AddComponentLibrary("https://waterservices.usgs.gov/nwis/");
                })
                .ConfigureLifecycleEvents(lifecycle => {
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
