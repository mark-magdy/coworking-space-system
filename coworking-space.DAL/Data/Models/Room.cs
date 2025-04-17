using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PrivatePrice { get; set; }
        public int MinimumPrivateCapacity { get; set; }
        [DefaultValue(60)]
        public int MinimumBookingTime { get; set; } // in minutes

        public decimal SharedPricePerPerson { get; set; }
        public decimal PrivatePricePerPerson { get; set; }

        public int Capacity { get; set; }
        public int CurrentCapacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get;  set; }

        

    }
}
