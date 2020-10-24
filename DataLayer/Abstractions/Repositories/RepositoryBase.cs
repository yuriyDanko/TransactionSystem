using DataLayer.Abstractions.Entities;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        protected DatabaseContext DbContext { get; set; }
        public RepositoryBase(DatabaseContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public async Task<ICollection<T>> FindAllAsync()
        {
            return await Task.FromResult(this.DbContext.Set<T>().AsNoTracking().ToList());
        }
        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(this.DbContext.Set<T>().Where(expression).AsNoTracking());
        }
        public async Task CreateAsync(T entity)
        {
            await this.DbContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            this.DbContext.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
             this.DbContext.Set<T>().Remove(entity);
             await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
