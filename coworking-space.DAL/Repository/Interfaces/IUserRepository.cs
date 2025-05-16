using CO_Working_Space;
using coworking_space.DAL.Data.Models;

namespace coworking_space.DAL.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        public Task<User> GetUserWithOrders(string id);
        public Task<User> GetUserWithReservations(string id);
        Task<List<User>> GetAllActiveUsers();
        public Task<User> GetUserByIdAsync(string id);

    }
}
