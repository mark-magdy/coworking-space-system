using CO_Working_Space;
using coworking_space.DAL.Data.Models;

namespace coworking_space.DAL.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        public Task<User> GetUserWithOrders(int id);
        public Task<User> GetUserWithReservations(int id);
    }
}
