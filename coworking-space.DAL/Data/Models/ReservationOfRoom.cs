using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class ReservationOfRoom
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ?EndDate { get; set; }
        public Status Status { get; set; } // e.g., "Pending", "Confirmed", "Cancelled"
        public string? SpecialRequests { get; set; } // e.g., "Vegan meal", "Late check-in"
        public string? Notes { get; set; } // e.g., "VIP guest", "Frequent customer"

     
        public decimal TotalPrice { get; set; }  // e.g., total cost of the reservation->>>acumulator and actual total at end

        //[NotMapped] need some code to make it best practise as total price used as accumlator not only attribute and Price till now for viewing
        public decimal PriceTillNow { get; set; } // price till now to show->>> computed ,variable by time

        public DateTime UpdatedPriceDate { get; set; } // e.g., date when the private room is calculated
        public bool IsPrivate { get; set; } 

        // Navigation properties
        public int RoomId { get; set; }
        public virtual Room Rooms { get; set; } // Assuming a reservation can be for multiple rooms

        public int TotalReservationsId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual TotalReservations TotalReservations { get; set; } // Assuming a reservation can be for multiple total reservations

    }
}
