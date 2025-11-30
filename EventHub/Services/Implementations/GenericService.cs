using EventHub.Repositories.Interfaces;
using EventHub.Services.Interfaces;
using System.Linq.Expressions;

namespace EventHub.Services.Implementations
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repo;

        public GenericService(IGenericRepository<T> repo)
        {
            _repo = repo;
        }
        public async Task CreateAsync(T entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _repo.Delete(entity);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repo.FindAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _repo.Update(entity);
            await _repo.SaveAsync();
        }
    }
}
