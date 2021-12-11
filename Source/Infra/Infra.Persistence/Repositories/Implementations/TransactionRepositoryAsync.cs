using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.DTOs.Filters;
using Application.Exceptions;
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

        public async Task<IReadOnlyList<Transaction>> GetByFilters(Expression<Func<Transaction, bool>> filters)
        {
            try
            {
                return await _transactions.Where(filters).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }
    }
}