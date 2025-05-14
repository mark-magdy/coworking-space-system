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
    public class TotalReservationsRepository : GenericRepository<TotalReservations>, ITotalReservationsRepository
    {
        public TotalReservationsRepository(Context context) : base(context) { }
        public TotalReservations getReservationsByid(int id)
        {
            return _context.TotalReservations
       .Include(tr => tr.Reservations)
           .ThenInclude(r => r.Rooms)
       .FirstOrDefault(tr => tr.Id == id);
        }
        public async Task<TotalReservations?> GetByIdWithReservationsAsync(int id)
        {
            return await _context.TotalReservations
                .Include(tr => tr.Reservations)
                .FirstOrDefaultAsync(tr => tr.Id == id);
        }
        public void AddReservation(ReservationOfRoom reservation, int id)
        {
            var totalReservation = _context.TotalReservations
                .Include(tr => tr.Reservations)
                .FirstOrDefault(tr => tr.Id == id);
            if (totalReservation != null)
            {
                totalReservation.Reservations.Add(reservation);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Total reservation not found");
            }

        }

    }
}
