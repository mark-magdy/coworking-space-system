using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Data;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coworking_space.DAL.Repository.Implementations
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        
        public RoomRepository(Context context) : base(context) { }
        public async Task<Room> getRoomByIdWithReservations(int id)
        {
            var ret = await _context.Rooms
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.ID == id);
            return ret;
        }
    }

}
