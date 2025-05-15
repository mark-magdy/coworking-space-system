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
    public class ReservationsRepository : GenericRepository<ReservationOfRoom>, IReservationsRepository
    {
        public ReservationsRepository(Context context) : base(context) { }

        public async Task<List<ReservationOfRoom?>> GetAllUpcomingReservationsWithRooms()
        {
            return await _context.ReservationOfRooms.Where(r => r.StartDate > DateTime.Now&& r.Status==Status.Pending)
                .Include(r => r.Rooms)
                 .Include(r => r.TotalReservations)
                    .ThenInclude(tr => tr.user)
                .ToListAsync();
        }
        public async Task<List<ReservationOfRoom>> GetUpcomingReservationsWithUserAsync(int roomId)
        {
            return await _context.ReservationOfRooms
                .Include(r => r.TotalReservations)
                    .ThenInclude(tr => tr.user)
                .Where(r => r.RoomId == roomId && r.StartDate > DateTime.Now)
                .ToListAsync();
        }

    }
}
