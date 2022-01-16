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

            // UseCase: Transaction
            services.AddScoped<IGetTransactionsUseCase, GetTransactionsUseCase>();
            services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        }
    }
}