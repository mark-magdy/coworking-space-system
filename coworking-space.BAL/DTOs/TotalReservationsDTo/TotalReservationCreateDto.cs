using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Dtos.TotalReservationsDTo
{
    public class TotalReservationCreateDto
    {
        
        public int UserId { get; set; }
    }
    public class ReservationCreateDto
    {

        public string SpecialRequests { get; set; }

        public string Notes { get; set; } // e.g., "VIP guest", "Frequent customer"

        public bool IsPrivate { get; set; }
        public int RoomId { get; set; } 


    }
}
