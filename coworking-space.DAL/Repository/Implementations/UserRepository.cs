using CO_Working_Space;
using coworking_space.DAL.Data;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace coworking_space.DAL.Repository.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }
        public async Task<User> GetUserWithOrders(int id)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                       .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User> GetUserWithReservations(int id)
        {
            var user = await _context.Users
                .Include(u => u.TotalReservations)
                    .ThenInclude(t=>t.Reservations)
                          .ThenInclude(r => r.Rooms)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User?> GetByEmailAsync(string email) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user ;
        }
        public async Task<List<User>> GetAllActiveUsers()
        {
            var users = await _context.Users
                .Where(u => u.IsActive)
                .Include(u => u.Orders)
                .Include(u => u.TotalReservations)
                .ToListAsync();
            return users;
        }
    }
}
