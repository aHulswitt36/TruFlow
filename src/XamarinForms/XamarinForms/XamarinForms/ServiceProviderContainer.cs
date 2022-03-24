

using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XamarinForms.Services;
using XamarinForms.Settings;
using XamarinForms.ViewModel;
using XamarinForms.Views;

namespace XamarinForms
{
    public static class ServiceProviderContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var file = assembly.GetManifestResourceStream(
                assembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("settings.json", StringComparison.OrdinalIgnoreCase))
            );
            using var sr = new StreamReader(file);
            var json = sr.ReadToEnd();
            var settings = JsonConvert.DeserializeObject<BaseSettings>(json);
            services.AddSingleton(settings.UsgsSettings);
            services.AddComponentLibrary("https://waterservices.usgs.gov/nwis/");
            services.AddTransient<River>();
            services.AddTransient<RiverViewModel>();
            services.AddTransient<Rivers>();
            services.AddTransient<RiversViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();

            return services;
        }

        public static IServiceCollection AddComponentLibrary(this IServiceCollection services, string baseUri)
        {
            services.AddHttpClient<IUsgsService, UsgsService>(httpClient => httpClient.BaseAddress = new Uri(baseUri));
            return services;
        }
    }
}
