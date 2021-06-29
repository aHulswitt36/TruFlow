using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.Settings;
using Infrastructure;

namespace blazor_web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var settings = builder.Configuration.GetSection("UsgsSettings").Get<UsgsSettings>();
            builder.Services.AddSingleton(settings);
            builder.Services.AddComponentLibrary(settings.BaseUrl);

            await builder.Build().RunAsync();
        }
    }
}
