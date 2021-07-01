using Infrastructure;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace maui_blazor
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .RegisterBlazorMauiWebView(typeof(Startup).Assembly)
                .UseMicrosoftExtensionsServiceProviderFactory()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureServices(services =>
                {
                    services.AddBlazorWebView();
                    var assembly = Assembly.GetExecutingAssembly();
                    var file = assembly.GetManifestResourceStream(
                        assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("settings.json", System.StringComparison.OrdinalIgnoreCase))
                    );
                    using var sr = new StreamReader(file);
                    var json = sr.ReadToEnd();
                    var settings = JsonConvert.DeserializeObject<Infrastructure.Settings.BaseSettings>(json);
                    services.AddSingleton(settings.UsgsSettings);
                    services.AddComponentLibrary("https://waterservices.usgs.gov/nwis/");
                });
        }
    }
}
