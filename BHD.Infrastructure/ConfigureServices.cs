using BHD.Application.Common.Interfaces.Caching;
using BHD.Application.Common.Interfaces.Services;
using BHD.Infrastructure.Caching;
using BHD.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using BHD.Application.Common.Interfaces;
using BHD.Infrastructure.Persistence;

namespace BHD.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IBHDDbContext, BHDContext>(options => options.UseInMemoryDatabase("BHDDb"));
        }
        else
        {
            services.AddDbContext<IBHDDbContext, BHDContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, builder => builder.MigrationsAssembly(typeof(BHDContext).Assembly.FullName)));
        }

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        services.AddScoped<ICacheManager, MemoryCacheManager>();
        return services;
    }
}