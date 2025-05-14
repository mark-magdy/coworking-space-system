using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.DTOs.PaymentDTO
{
    public class CreatPaymentDTO
    {
        public int? OrderId { get; set; }
        public int? TotalReservationsId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }

    }
}
