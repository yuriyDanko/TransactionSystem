using DataLayer.Contexts;
using Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Abstractions.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        protected DatabaseContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        public RepositoryBase(DatabaseContext dbContext)
        {
            this.DbContext = dbContext;
            DbSet = this.DbContext.Set<T>();
        }
        public async Task<ICollection<T>> FindAllAsync()
        {
            return await Task.FromResult(DbSet.ToList());
        }
        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(result: DbSet.Where(expression).AsNoTracking());
        }
        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
             DbSet.Remove(entity);
             await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
