using coworking_space.DAL.Data;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Repository.Implementations {
    public class OrderRepository : GenericRepository<Order>, IOrderRepository {
        public OrderRepository(Context context) : base(context) {
        }
        public override async Task<IEnumerable<Order>> GetAllAsync(int? page = null, int? pageSize = null) {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.User) // optional
                .AsQueryable();

            if (page.HasValue && pageSize.HasValue) {
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}
