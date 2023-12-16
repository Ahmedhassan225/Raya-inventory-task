using Application.Helpers;
using Application.Helpers.Interfaces;
using Application.Repository;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;

namespace Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()))
                .AddScoped<IPdfHelper, PdfHelper>()
                .AddRepository()
                .AddUnitOfWork();
        }
    }
}

