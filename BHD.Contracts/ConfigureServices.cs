using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BHD.Contracts
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddContracts(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
