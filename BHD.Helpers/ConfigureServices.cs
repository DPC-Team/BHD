using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BHD.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
