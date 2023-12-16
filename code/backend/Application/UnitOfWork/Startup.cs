using Application.UnitOfWork;
using Application.UnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    internal static class Startup
    {
        internal static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IProductUOW, ProductUOW>()
                .AddScoped<ILookupUOW, LookupUOW>()
                .AddScoped<ITransactionUOW, TransactionUOW>();
        }
    }
}