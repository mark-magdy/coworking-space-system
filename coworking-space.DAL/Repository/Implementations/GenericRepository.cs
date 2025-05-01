using coworking_space.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coworking_space.DAL.Data;
namespace coworking_space.DAL.Repository.Implementations {
    public class GenericRepository<T> : IGenericRepository<T> where T : class {
        protected readonly Context _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(Context context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(int? page = null, int? pageSize = null) {
            IQueryable<T> query = _dbSet;

            if (page.HasValue && pageSize.HasValue) {
                int skip = (page.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
        public  async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<T> AddAsync(T entity) {
            await _dbSet.AddAsync(entity);   // Add entity to DbSet
            return entity;  // Return the inserted entity (with any updated values, like the generated ID)
        }
        public void Update(T entity) {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Update(entity);
        }
        public void Delete(T entity) {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }
        public async Task DeleteByIdAsync(int id) {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with Id {id} was not found.");

            _dbSet.Remove(entity);
        }
        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync()>0;
    }

}
