using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MoneyManager.Domain.Models;
using MoneyManager.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Database.Services
{
    public class GenericDataService<T> : IDataService<T> where T : EntityObject
    {
        private readonly MoneyManagerDbContextFactory mContext;

        public GenericDataService(MoneyManagerDbContextFactory context)
        {
            mContext = context;
        }

        public async Task<T> Create(T entity)
        {
            using(MoneyManagerDbContext context = mContext.CreateDbContext())
            { 
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (MoneyManagerDbContext context = mContext.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (MoneyManagerDbContext context = mContext.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (MoneyManagerDbContext context = mContext.CreateDbContext())
            {
                IEnumerable<T> entitys = await context.Set<T>().ToListAsync();
                return entitys;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (MoneyManagerDbContext context = mContext.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
