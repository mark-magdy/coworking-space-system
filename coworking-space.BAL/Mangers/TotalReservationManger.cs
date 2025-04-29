using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.BAL.MangerInterfaces;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Mangers
{

    public class TotalReservationManger : ITotalReservationsManger
    {
        private readonly ITotalReservationsRepository _reservationRepository;

        const decimal pricePerHourShared = 10; // Example price per hour
        public TotalReservationManger(ITotalReservationsRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public TotalReservationsReadDto GetTotalReservations(int id, Status status)
        {
            var totalReservation = _reservationRepository.getReservationsByid(id);
            var reservations_status = totalReservation.Reservations.Where(r => r.Status == status).ToList();//the list of status i want

            if (totalReservation == null)
                throw new KeyNotFoundException("Reservation not found.");

            //logic of calculating   //computing Pricetillnow for each reservation
            
            //the logic of updating the total price of each reservation is executed when one left the room
            //while leaving the current capacity of room is updated and total price and update date of each reservation  ->postprocess

            //for now this is for not paid status
            decimal totalPrice = 0;
            DateTime timeNow = DateTime.Now;
            foreach (var reservation in totalReservation.Reservations)
            {
               
                if (  reservation.EndDate != null&&reservation.EndDate < timeNow) //ended but not paid 
                {
                    reservation.PriceTillNow = reservation.TotalPrice;
                    totalPrice += reservation.TotalPrice;
                }
                else 
                { 
                    decimal price = 0;
                    if (reservation.IsPrivate) {
                        price = reservation.TotalPrice;
                        decimal timeSpan = (decimal)(timeNow - reservation.UpdatedPriceDate).TotalHours;
                        var room = reservation.Rooms;

                        if (room.CurrentCapacity > room.Capacity)
                            price += (reservation.Rooms.PrivatePricePerPerson * timeSpan);

                        else
                            price += (reservation.Rooms.PrivatePrice / room.CurrentCapacity);
                        reservation.PriceTillNow = price;
                    }
                    else //shared
                    {
                        price = reservation.TotalPrice;
                        decimal timeSpan = (decimal)(timeNow - reservation.StartDate).TotalHours;
                        price += (reservation.Rooms.SharedPricePerPerson * timeSpan);
                        reservation.PriceTillNow = price;
                    }
                    totalPrice += price;
                } 
            }

            return new TotalReservationsReadDto
            {

                description = totalReservation.Description,
                totalPrice = totalPrice,
                reservations = totalReservation.Reservations
            .Where(r => r.Status == status) // ✅ Business logic lives here
            .Select(r => new ReservationReadDto
            {
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Status = r.Status,
                Notes = r.Notes,
                PriceTillNow = r.PriceTillNow,
                IsPrivate = r.IsPrivate,
                Rooms = new RoomReadDto
                {
                    Name = r.Rooms.Name,
                    Description = r.Rooms.Description
                }
            }).ToList()
            };
        }


    }
 }


