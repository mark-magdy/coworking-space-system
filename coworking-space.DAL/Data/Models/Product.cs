using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Boolean IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        //public Boolean IsAvailable { get;private set; }
        
        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; } 
    }
}
