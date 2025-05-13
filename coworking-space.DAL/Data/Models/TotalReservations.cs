using CO_Working_Space;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class TotalReservations
    {
        public int Id {  get; set; }
        public string? Description { get; set; } /// <summary>
       
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ?EndDate { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [EnumDataType(typeof(Status))]

        public Status Status { get; set; } // e.g., "Pending", "Confirmed", "Cancelled"

        // navigation property 
        public virtual ICollection<ReservationOfRoom>? Reservations { get; set; }//total price is calculated from each reservation
        public User user { get; set; }  
        public int UserId { get; set; }
        public virtual Payment? Payment { get; set; }


    }
}
