
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

        public async Task ProcessBulkPaymentAsync(CreateBulkPaymentDTO dto)
        {
            if (!dto.PaymentMethod.Equals("Cash", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Only 'Cash' payments are accepted at this time.");

            if ((dto.OrderIds == null || !dto.OrderIds.Any()) &&
                (dto.TotalReservationIds == null || !dto.TotalReservationIds.Any()))
                throw new ArgumentException("At least one OrderId or TotalReservationId must be provided.");

            decimal totalAmount = 0;
            var orders = new List<Order>();
            var reservations = new List<TotalReservations>();

            if (dto.OrderIds != null)
            {
                foreach (var orderId in dto.OrderIds)
                {
                    var order = await _orderRepo.GetByIdAsync(orderId)
                        ?? throw new ArgumentException($"Order with ID {orderId} not found.");

                    if (order.Order_Status == Status.Completed)
                        throw new InvalidOperationException($"Order {orderId} is already paid.");

                    totalAmount += order.TotalPrice;
                    orders.Add(order);
                }
            }

            if (dto.TotalReservationIds != null)
            {
                foreach (var reservationId in dto.TotalReservationIds)
                {
                    var reservation = await _reservationRepo.GetByIdWithReservationsAsync(reservationId)
                        ?? throw new ArgumentException($"Reservation with ID {reservationId} not found.");

                    if (reservation.Status == Status.Completed)
                        throw new InvalidOperationException($"Reservation {reservationId} is already paid.");

                    if (reservation.Reservations == null || !reservation.Reservations.Any())
                        throw new ArgumentException($"No reservations found under TotalReservation {reservationId}.");

                    totalAmount += reservation.Reservations.Sum(r => r.TotalPrice);
                    reservations.Add(reservation);
                }
            }

            if (dto.Amount < totalAmount)
                throw new ArgumentException($"Insufficient payment. Required: {totalAmount}, Provided: {dto.Amount}");

            var payment = new Payment
            {
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                PaymentDate = DateTime.Now,
                PaymentStatus = Status.Completed,
                PaymentDetails = $"Payment for multiple orders/reservations",
                PaymentType = "One-time",
                Currency = "EGP",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRefunded = false
            };

            foreach (var order in orders)
            {
                order.Order_Status = Status.Completed;
                _orderRepo.Update(order);
            }

            foreach (var reservation in reservations)
            {
                reservation.Status = Status.Completed;
                _reservationRepo.Update(reservation);

                var detailedReservation = _reservationRepo.getReservationsByid(reservation.Id);
                foreach (var res in detailedReservation.Reservations)
                {
                    res.Status = Status.Completed;
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
                throw new InvalidOperationException("An error occurred while processing the bulk payment.", ex);
            }
        }
        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year)
        {
            var payments = await _paymentRepo.GetPaymentsByMonthAsync(month, year);
            return payments.Where(p => p.PaymentStatus == Status.Completed).Sum(p => p.Amount);
        }
        public async Task<decimal> GetDailyRevenueAsync(int day, int month, int year)
        {
            var payments = await _paymentRepo.GetPaymentsByDayAsync(day, month, year);
            return payments
                .Where(p => p.PaymentStatus == Status.Completed)
                .Sum(p => p.Amount);
        }

    }
}

