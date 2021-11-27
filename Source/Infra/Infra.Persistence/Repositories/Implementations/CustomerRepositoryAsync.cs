using Application.Interfaces;
using Domain.Entities;
using Infra.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Repositories
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {
        private readonly DbSet<Customer> _customers;

        public CustomerRepositoryAsync(AppDbContext dbContext) : base(dbContext) => _customers = dbContext.Set<Customer>();
    }
}