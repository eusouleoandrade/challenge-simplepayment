using Application.Interfaces;
using Domain.Entities;
using Infra.Persistence.Repositories;

namespace Infra.Persistence.Repositories
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {   
    }
}