using CO_Working_Space;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coworking_space.DAL.Data.Models
{
    public enum Status
    {
        Pending,
        Completed,
        Cancelled,
        Refunded
    }
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreatedAt { get; set; } 
        [Column(TypeName = "nvarchar(20)")]
        [EnumDataType(typeof(Status))]
        public Status Order_Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderDetails { get; set; } // Additional details about the order
        bool InOut { get; set; } // Indicates if the order is in or out

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; } 
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Payment Payment { get; set; }


    }
}




 