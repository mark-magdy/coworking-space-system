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
        public TotalReservationsReadDto? GetTotalReservations(int id);
        public ReservationReadDto AddReservation(ReservationCreateDto reservationCreateDto,int id);
        public Task<TotalReservationsReadDto> MakeTotalReservation(TotalReservationCreateDto totalReservationCreateDto);
        public Task<ReservationReadDto> UpdateReservation(int id, ReservationUpdateDto reservationUpdateDto);
        Task<bool> DeleteTotalReservationAsync(int id);
        Task<bool> DeleteReservationAsync(int totalReservationId, int reservationId);
        ReservationReadDto? GetReservationFromTotalReservation(int totalReservationId, int reservationId);
        Task<List<TotalReservationsReadDto>> GetAllTotalReservationsAsync();
        Task<List<UpcomingReservationReadIDDto>> GetUpcomingReservationsAsync(int roomId);
    }
}

