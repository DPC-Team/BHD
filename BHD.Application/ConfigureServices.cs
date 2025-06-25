using BHD.Application.Common.Bahaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BHD.Application.Services.Interfaces;
using BHD.Application.Services;

namespace BHD.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //Services
            services.AddScoped<ICuentasService, CuentasServices>();
            services.AddScoped<ITransaccionesService, TransaccionesService>();

            return services;
        }
    }
}
