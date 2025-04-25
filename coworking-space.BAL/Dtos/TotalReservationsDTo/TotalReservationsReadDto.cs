using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Dtos.TotalReservationsDTo
{
    public class TotalReservationsReadDto
    {
        public string description { get; set; }
        public decimal totalPrice { get; set; }
        public List<ReservationReadDto> reservations { get; set; }

    }
    public class ReservationReadDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public string Notes { get; set; } // e.g., "VIP guest", "Frequent customer"
       
        public decimal PriceTillNow { get; set; } // e.g., price per hour for the reservation
       
        public bool IsPrivate { get; set; }
        public RoomReadDto Rooms { get; set; }

    }
    public class RoomReadDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
