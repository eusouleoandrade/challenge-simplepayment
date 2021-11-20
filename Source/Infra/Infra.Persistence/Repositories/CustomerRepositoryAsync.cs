using Application.Interfaces;
using Domain.Entities;
using Infra.Persistence.Repositories.Common;

namespace Infra.Persistence.Repositories
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {   
    }
}