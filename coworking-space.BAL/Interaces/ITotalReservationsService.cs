using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Interaces
{
    public interface ITotalReservationsService
    {
        public TotalReservationsReadDto GetTotalReservations(int id, Status status);
        public  ReservationOfRoom AddReservation(ReservationCreateDto reservationCreateDto,int id);
        public  Task<TotalReservations> MakeTotalReservation(TotalReservationCreateDto totalReservationCreateDto);
    }
}
