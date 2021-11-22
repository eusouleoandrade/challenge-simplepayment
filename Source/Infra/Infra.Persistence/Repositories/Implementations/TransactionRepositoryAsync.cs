using Application.Interfaces;
using Domain.Entities;

namespace Infra.Persistence.Repositories
{
    public class TransactionRepositoryAsync : GenericRepositoryAsync<Transaction>, ITransactionRepositoryAsync
    {
    }
}