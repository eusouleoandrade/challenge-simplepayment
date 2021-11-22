using Application.Interfaces;
using Infra.Persistence.Contexts;
using Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence.IoC
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // Repositories
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<ICustomerRepositoryAsync, CustomerRepositoryAsync>();
            services.AddScoped<ITransactionRepositoryAsync, TransactionRepositoryAsync>();
        }
    }
}