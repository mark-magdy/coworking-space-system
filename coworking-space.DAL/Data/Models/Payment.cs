using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class Payment
    {

        public int Id { get; set; }
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; } // Unique identifier for the transaction
        [Column(TypeName = "nvarchar(20)")]
        [EnumDataType(typeof(Status))]
        public Status PaymentStatus { get; set; } // e.g., Completed, Failed, Pending
        public string PaymentDetails { get; set; } // Additional details about the payment
        [DefaultValue("One-time")]
        public string PaymentType { get; set; } // e.g., One-time, Recurring

        [Column(TypeName = "nvarchar(3)")]
        [DefaultValue("EGP")]
        public string Currency { get; set; } // e.g., USD, EUR
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public bool IsRefunded { get; set; } = false; // Indicates if the payment has been refunded
        public DateTime? RefundDate { get; set; } // Date of refund if applicable
        // string RefundTransactionId { get; set; } // Unique identifier for the refund transaction
        public string RefundReason { get; set; } // Reason for the refund


        // Navigation properties
       
        public int OrderId { get; set; }
        public Order Order { get; set; }   
        public TotalReservations TotalReservations { get; set; }
        public int TotalReservationsId { get; set; } // Foreign key for TotalReservations

    }
}
