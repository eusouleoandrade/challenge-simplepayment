using Application.Interfaces;
using Domain.Entities;
using Infra.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Repositories
{
    public class TransactionRepositoryAsync : GenericRepositoryAsync<Transaction>, ITransactionRepositoryAsync
    {
        private readonly DbSet<Transaction> _transactions;

        public TransactionRepositoryAsync(AppDbContext dbContext) : base(dbContext) => _transactions = dbContext.Set<Transaction>();
    }
}