using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.OrderDTO {
    public class OrderUpdateDto {
        public Status Order_Status { get; set; }
        public string OrderDetails { get; set; }
        public bool InOut { get; set; }
    }

}
