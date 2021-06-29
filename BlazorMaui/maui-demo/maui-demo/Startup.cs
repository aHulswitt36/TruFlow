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
                .ConfigureHostConfiguration(host =>
                {
                    host.AddJsonFile("~/appsettings.json");
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<UsgsSettings>(o => hostContext.Configuration.GetSection("UsgsSettings"));
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
