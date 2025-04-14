using CO_Working_Space;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coworking_space.DAL.Data.Models
{
    public enum Status
    {
        Pending,
        Completed,
        Cancelled
    }
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime date { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [EnumDataType(typeof(Status))]
        public Status Order_Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int OrderItemId { get; set; }
        // Navigation properties

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
       

    } }




 