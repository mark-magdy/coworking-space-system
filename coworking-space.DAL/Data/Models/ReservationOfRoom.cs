using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class ReservationOfRoom
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public Status Status { get; set; } // e.g., "Pending", "Confirmed", "Cancelled"
        public string SpecialRequests { get; set; } // e.g., "Vegan meal", "Late check-in"
        public string Notes { get; set; } // e.g., "VIP guest", "Frequent customer"
        public decimal TotalPrice { get; set; } // e.g., total cost of the reservation
        public decimal PricePerUpdate { get; set; } // e.g., price per hour for the reservation
        public bool IsPrivate { get; set; } 

        // Navigation properties
        public int RoomId { get; set; }
        public virtual Room Rooms { get; set; } // Assuming a reservation can be for multiple rooms

    }
}
