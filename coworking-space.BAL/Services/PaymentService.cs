//using coworking_space.BAL.DTOs.PaymentDTO;
//using coworking_space.BAL.Interaces;
//using coworking_space.DAL.Data.Models;
//using coworking_space.DAL.Repository.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace coworking_space.BAL.Services
//{
//    public class PaymentService : IPaymentService
//    {
//        private readonly IOrderRepository _orderRepo;
//        private readonly ITotalReservationsRepository _reservationRepo;
//        private readonly IPaymentRepository _paymentRepo;
//        private readonly IReservationsRepository _resRepository;

//        public PaymentService(
//            IOrderRepository orderRepo,
//            ITotalReservationsRepository reservationRepo,
//            IPaymentRepository paymentRepo,
//            IReservationsRepository resRepository)
//        {
//            _orderRepo = orderRepo;
//            _reservationRepo = reservationRepo;
//            _paymentRepo = paymentRepo;
//            _resRepository = resRepository;
//        }

//        public async Task<Payment> ProcessPaymentAsync(CreatPaymentDTO dto)
//        {
//            if (!dto.PaymentMethod.Equals("Cash", StringComparison.OrdinalIgnoreCase))
//                throw new ArgumentException("Only 'Cash' payments are accepted at this time.");

//            if (dto.OrderId == null && dto.TotalReservationsId == null)
//                throw new ArgumentException("At least one of OrderId or TotalReservationsId must be provided.");

//            decimal totalAmount = 0;
//            Order order = null;
//            TotalReservations reservation = null;

//            // Check for OrderId and add the price
//            if (dto.OrderId != null)
//            {
//                order = await _orderRepo.GetByIdAsync(dto.OrderId.Value);
//                if (order == null)
//                    throw new ArgumentException("Order not found.");

//                totalAmount += order.TotalPrice;
//            }
//            else
//            {
//              order = null; // Ensure no reservation is set if there's no TotalReservationsId
//            }

//            // Check for TotalReservationsId and add the price
//            if (dto.TotalReservationsId != null)
//            {
//                reservation = await _reservationRepo.GetByIdAsync(dto.TotalReservationsId.Value);
//                if (reservation == null)
//                    throw new ArgumentException("Reservation not found.");

//                totalAmount += reservation.Price;
//            }
//            else
//            {
//                reservation = null; // Ensure no reservation is set if there's no TotalReservationsId
//            }

//            // Validate the provided amount
//            if (dto.Amount < totalAmount)
//                throw new ArgumentException($"Insufficient payment. Required: {totalAmount}, Provided: {dto.Amount}");

//            // Create the Payment object
//            var payment = new Payment
//            {
//                Order = order,
//                TotalReservations = reservation,
//                PaymentMethod = dto.PaymentMethod,
//                Amount = dto.Amount,
//                PaymentDate = DateTime.Now,
//                TransactionId = null, // Make sure to handle this properly if needed
//                PaymentStatus = Status.Completed,
//                PaymentDetails = $"Payment for {(order != null ? $"Order #{order.Id} " : "")}{(reservation != null ? $"Reservation #{reservation.Id}" : "")}",
//                PaymentType = "One-time",
//                Currency = "EGP",
//                CreatedAt = DateTime.Now,
//                UpdatedAt = DateTime.Now,
//                IsRefunded = false,
//               RefundReason = null
//            };

//            // Update statuses
//            if (order != null && order.Order_Status != Status.Completed)
//                order.Order_Status = Status.Completed;

//            if (reservation != null)
//            {
//                var detailedReservation = _reservationRepo.getReservationsByid(reservation.Id);
//                foreach (var res in detailedReservation.Reservations)
//                {
//                    res.Status = Status.Completed;
//                    _resRepository.Update(res); // Ensure each reservation is updated properly

//                }
//            }

//            try
//            {
//                // Save changes in a transaction
//                await _paymentRepo.AddAsync(payment);
//               // await _paymentRepo.SaveAsync();// Save the payment first

//                if (order != null)
//                {
//                    _orderRepo.Update(order);           // Update the order if not null
//                          // Save the order changes
//                }


//                await _orderRepo.SaveAsync();
//                return payment; // Return the payment object after processing
//            }
//            catch (Exception ex)
//            {
//                // Log the error or handle appropriately
//                throw new InvalidOperationException("An error occurred while processing the payment.", ex);
//            }
//        }
//    }
//}







using coworking_space.BAL.DTOs.PaymentDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ITotalReservationsRepository _reservationRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IReservationsRepository _resRepository;

        public PaymentService(
            IOrderRepository orderRepo,
            ITotalReservationsRepository reservationRepo,
            IPaymentRepository paymentRepo,
            IReservationsRepository resRepository)
        {
            _orderRepo = orderRepo;
            _reservationRepo = reservationRepo;
            _paymentRepo = paymentRepo;
            _resRepository = resRepository;
        }


        public async Task ProcessPaymentAsync(CreatPaymentDTO dto)
        {
            if (!dto.PaymentMethod.Equals("Cash", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Only 'Cash' payments are accepted at this time.");

            if (dto.OrderId == null && dto.TotalReservationsId == null)
                throw new ArgumentException("At least one of OrderId or TotalReservationsId must be provided.");

            decimal totalAmount = 0;
            Order order = null;
            TotalReservations reservation = null;

            if (dto.OrderId != null)
            {
                order = await _orderRepo.GetByIdAsync(dto.OrderId.Value)
                     ?? throw new ArgumentException("Order not found.");
                totalAmount += order.TotalPrice;
            }

            if (dto.TotalReservationsId != null)
            {
                reservation = await _reservationRepo.GetByIdWithReservationsAsync(dto.TotalReservationsId.Value)
                  ?? throw new ArgumentException("Reservation not found.");


                if (reservation.Reservations == null || !reservation.Reservations.Any())
                    throw new ArgumentException("No reservations found under this TotalReservation.");

                totalAmount += reservation.Reservations.Sum(r => r.TotalPrice);
            }


            if (dto.Amount < totalAmount)
                throw new ArgumentException($"Insufficient payment. Required: {totalAmount}, Provided: {dto.Amount}");

            if (order != null && order.Order_Status == Status.Completed)
                throw new InvalidOperationException("This order is already paid.");

            if (reservation != null && reservation.Status == Status.Completed)
                throw new InvalidOperationException("This reservation is already paid.");

            var payment = new Payment
            {
                Order = order,
                TotalReservations = reservation,
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                PaymentDate = DateTime.Now,
                PaymentStatus = Status.Completed,
                PaymentDetails = $"Payment for {(order != null ? $"Order #{order.Id} " : "")}{(reservation != null ? $"Reservation #{reservation.Id}" : "")}",
                PaymentType = "One-time",
                Currency = "EGP",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRefunded = false,
                RefundReason = null
            };

            if (order != null)
            {
                order.Order_Status = Status.Completed;
                _orderRepo.Update(order);
            }

            if (reservation != null)
            {
                reservation.Status = Status.Completed;

                _reservationRepo.Update(reservation);

                var detailedReservation = _reservationRepo.getReservationsByid(reservation.Id);
                foreach (var res in detailedReservation.Reservations)
                {
                    res.Status = Status.Completed;
                   // res.EndDate = DateTime.Now; 
                   _resRepository.Update(res);
                }
            }

            try
            {
                await _paymentRepo.AddAsync(payment);
                await _orderRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while processing the payment.", ex);
            }
        }
    }
}

