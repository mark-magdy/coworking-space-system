using System.ComponentModel.DataAnnotations.Schema;

namespace coworking_space.DAL.Data.Models
{
    public enum OrderStatus
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
        public OrderStatus Status { get; set; }




    }
}
