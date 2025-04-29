using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Repository.Interfaces {
    public interface IGenericRepository<T> where T : class {
        Task<IEnumerable<T>> GetAllAsync(int? page = null, int? pageSize = null);
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task DeleteByIdAsync(int id);
        Task<bool> SaveAsync();
    }
}
