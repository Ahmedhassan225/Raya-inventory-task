using Infrastructure.Context.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    internal static class Startup
    {

        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {    
            services
                .AddDbContext<RayaDBContext>(options => options.UseSqlServer(config.GetConnectionString("DB")))
                .AddScoped<DbContext, RayaDBContext>();

            return services;
        }
    }
}