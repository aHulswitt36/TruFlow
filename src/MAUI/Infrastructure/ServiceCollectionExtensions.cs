using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
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
