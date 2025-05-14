using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.OrderDTO {
    public class OrderReadDto {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Order_Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderDetails { get; set; }
        public bool InOut { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public List<OrderItemReadDto> OrderItems { get; set; } = new();
    }

    public class OrderItemReadDto {
        //public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
