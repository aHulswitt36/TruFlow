using System;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

namespace Components
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddComponentLibrary(this IServiceCollection services, string baseUri)
        {            
            services.AddHttpClient<IUsgsService, UsgsService>(httpClient => httpClient.BaseAddress = new Uri(baseUri));
            return services;
        }
        
    }
}
