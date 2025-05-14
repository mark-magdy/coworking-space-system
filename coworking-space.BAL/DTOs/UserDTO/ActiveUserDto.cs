using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.UserDTO
{
    public class ActiveUserDto
    {
        public int  id { get; set; }
        public string user { get; set; }
        public string space { get; set; }
        public DateTime date { get; set; }
        public TimeSpan  time { get; set; }
        public decimal? reservations { get; set; }
        public decimal ?orders { get; set; }
     
        public decimal total { get; set; }
    
    }
//    id: 3,
//      user: 'Michael Brown',
//      space: 'Conference Room',
//      date: 'Today',
//      time: '2:00 PM - 4:00 PM'
//reservations: 70,
//orders: 100  , 
//orderId: 12 , 
//reservationId: 2 
//total:  170 , 
}
