using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Dtos.TotalReservationsDTo
{
    public class ReservationUpdateDto
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
       
        public Status? Status { get; set; } // e.g., "Pending", "Confirmed", "Cancelled"
        public string? SpecialRequests { get; set; } // e.g., "Vegan meal", "Late check-in"
        public string? Notes { get; set; } // e.g., "VIP guest", "Frequent customer"

    }
}
