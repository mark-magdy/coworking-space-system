using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Repository.Interfaces
{
    public interface ITotalReservationsRepository : IGenericRepository<TotalReservations> {
        public TotalReservations getReservationsByid(int id);
        public TotalReservations getReservationsByUserId(int userId);
        public void AddReservation(ReservationOfRoom reservation,int id);
       
    }
}
