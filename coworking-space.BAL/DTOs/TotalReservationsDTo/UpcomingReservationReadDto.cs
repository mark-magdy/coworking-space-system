using coworking_space.BAL.Dtos.TotalReservationsDTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.TotalReservationsDTo
{
    public class UpcomingReservationReadDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
      
        public string? Notes { get; set; } // e.g., "VIP guest", "Frequent customer"

      
        public RoomReadReservationDto Rooms { get; set; }
        public UserUpcomingReservationReadDto Users { get; set; }
    }
    public class UserUpcomingReservationReadDto
    {
        
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }


    }
}
