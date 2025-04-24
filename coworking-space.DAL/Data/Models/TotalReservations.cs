using CO_Working_Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class TotalReservations
    {
        public int Id {  get; set; }
        public string Description { get; set; }
        // navigation property 
        public virtual ICollection<ReservationOfRoom> Reservations { get; set; }//total price is calculated from each reservation
        public User user { get; set; }  
        public int UsrId { get; set; }
        public virtual Payment Payment { get; set; }


    }
}
