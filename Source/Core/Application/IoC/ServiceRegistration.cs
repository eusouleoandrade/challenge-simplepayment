using System.Reflection;
using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            // Assembly 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // UseCase
            services.AddScoped<IGetTransactionsUseCase, GetTransactionsUseCase>();
        }
    }
}