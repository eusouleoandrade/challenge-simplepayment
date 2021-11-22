using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Infra.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext
                 .Set<T>()
                 .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        protected void DetachLocal(Func<T, bool> predicate)
        {
            var local = _dbContext.Set<T>().Local.Where(predicate).FirstOrDefault();

            if (!(local is null))
                _dbContext.Entry(local).State = EntityState.Detached;
        }
    }
}