using GetConnection.Core.Repositories.Base;
using GetConnection.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.Base
{
 
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly GetConnectionContext _getConnectionContext;
        public Repository(GetConnectionContext getConnectionContext)
        {
            _getConnectionContext = getConnectionContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _getConnectionContext.Set<T>().AddAsync(entity);
            await _getConnectionContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _getConnectionContext.Set<T>().Remove(entity);
            await _getConnectionContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _getConnectionContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _getConnectionContext.Set<T>().FindAsync(id);
        }
        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
