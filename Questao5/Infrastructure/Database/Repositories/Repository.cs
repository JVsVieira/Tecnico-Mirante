using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity<T>
    {
        protected readonly DataBaseContext _dbContext;
        public Repository(DataBaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> GetAsync(string id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> AddAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item.Id;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public T Edit(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return item;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
