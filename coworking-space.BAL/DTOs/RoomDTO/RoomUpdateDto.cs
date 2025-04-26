using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.RoomDTO {
    public class RoomUpdateDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PrivatePrice { get; set; }
        public int MinimumPrivateCapacity { get; set; }
        public int MinimumBookingTime { get; set; } // in minutes

        public decimal SharedPricePerPerson { get; set; } //default prices
        public decimal PrivatePricePerPerson { get; set; }//default prices
        public int Capacity { get; set; }
        public int CurrentCapacity { get; set; }
        public string? ImageUrl { get; set; }

        
    }
}
