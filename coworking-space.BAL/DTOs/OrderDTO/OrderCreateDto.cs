using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.OrderDTO {
    public class OrderCreateDto {
        public DateTime CreatedAt { get; set; }
        public Status Order_Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderDetails { get; set; }
        public bool InOut { get; set; }
        public int UserId { get; set; }

        public List<OrderItemCreateDto> OrderItems { get; set; } = new();
    }

    public class OrderItemCreateDto {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
