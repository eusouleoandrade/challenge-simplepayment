using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionRepositoryAsync : IGenericRepositoryAsync<Transaction>
    {   
        Task<IReadOnlyList<Transaction>> GetByFilters(Expression<Func<Transaction, bool>> filters);
    }
}