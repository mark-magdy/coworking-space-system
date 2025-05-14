using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.BAL.DTOs.TotalReservationsDTo;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services
{

    public class TotalReservationService : ITotalReservationsService
    {
        private readonly ITotalReservationsRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationsRepository _reserveRepo;


        const decimal pricePerHourShared = 10; // Example price per hour
        public TotalReservationService(ITotalReservationsRepository reservationRepository, IRoomRepository roomRepository, IReservationsRepository reservrepo)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _reserveRepo = reservrepo;

        }
        public async Task<List<TotalReservationsReadDto>> GetAllTotalReservationsAsync()
        {
            var totalReservations = await _reservationRepository.GetAllAsync();
            if (totalReservations == null || !totalReservations.Any())
            {
                return new List<TotalReservationsReadDto>();
            }
            //the paid total reservations are not included in the list of total reservations
            return totalReservations.Select(tr => GetTotalReservations(tr.Id)).ToList();//this for not paid total reservations
        }
        public TotalReservationsReadDto? GetTotalReservations(int id)//calculation of the current total reservation 
        {
            var totalReservation = _reservationRepository.getReservationsByid(id);
            //  var reservations_status = totalReservation.Reservations.Where(r => r.Status == status).ToList();//the list of status i want

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

                if (reservation.Status == Status.Completed) //ended but not paid 
                {
                    reservation.PriceTillNow = reservation.TotalPrice;
                    totalPrice += reservation.TotalPrice;
                }
                else if (reservation.Status == Status.Pending)
                {
                    decimal price = 0;
                    if (reservation.IsPrivate)
                    {
                        price = reservation.TotalPrice;
                        decimal timeSpan = (decimal)(timeNow - reservation.UpdatedPriceDate).TotalHours;
                        var room = reservation.Rooms;

                        if (room.CurrentCapacity > room.MinimumPrivateCapacity)
                            price += reservation.Rooms.PrivatePricePerPerson * timeSpan;

                        else
                            price += (reservation.Rooms.PrivatePrice / room.CurrentCapacity) * timeSpan;
                        reservation.PriceTillNow = price;
                    }
                    else //shared
                    {
                        price = reservation.TotalPrice;
                        decimal timeSpan = (decimal)(timeNow - reservation.StartDate).TotalHours;
                        price += reservation.Rooms.SharedPricePerPerson * timeSpan;
                        reservation.PriceTillNow = price;
                    }
                    totalPrice += price;
                }
            }

            var ret = new TotalReservationsReadDto
            {
                Id = totalReservation.Id,
                description = totalReservation.Description,
                totalPrice = totalPrice,
                reservations = totalReservation.Reservations
             //.Where(r => r.Status == status) // ✅ Business logic lives here
             .Select(r => new ReservationReadDto
             {
                 Id = r.Id,
                 StartDate = r.StartDate,
                 EndDate = r.EndDate.HasValue ? r.EndDate.Value : default(DateTime),
                 Status = r.Status,
                 Notes = r.Notes,
                 PriceTillNow = r.PriceTillNow,
                 IsPrivate = r.IsPrivate,
                 Rooms = new RoomReadReservationDto
                 {
                     Id = r.Rooms.ID,
                     Name = r.Rooms.Name,
                     Description = r.Rooms.Description
                 }
             }).ToList()
            };
            return ret;
        }
        public ReservationReadDto? GetReservationFromTotalReservation(int totalReservationId, int reservationId)
        {
            //need logic 
            var totalReservation = _reservationRepository.getReservationsByid(totalReservationId);
            if (totalReservation == null)
            {
                return null;
            }

            var reservation = totalReservation.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null)
            {
                return null;
            }

            return new ReservationReadDto
            {
                Id = reservation.Id,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate.HasValue ? reservation.EndDate.Value : default(DateTime),
                Status = reservation.Status,
                Notes = reservation.Notes,
                PriceTillNow = reservation.TotalPrice,
                IsPrivate = reservation.IsPrivate,
                Rooms = new RoomReadReservationDto
                {
                    Id = reservation.Rooms.ID,
                    Name = reservation.Rooms.Name,
                    Description = reservation.Rooms.Description
                }
            };
        }

        public ReservationReadDto AddReservation(ReservationCreateDto reservationCreateDto, int userId)
        {

            var totalreservation = _reservationRepository.getReservationsByUserId(userId);
            if (totalreservation == null || reservationCreateDto.StartDate.Date != DateTime.Today)
            {
                TotalReservationsReadDto totalReservationDto = MakeTotalReservation(new TotalReservationCreateDto
                {
                    UserId = userId,

                }).Result;
                totalreservation = new TotalReservations
                {
                    Id = totalReservationDto.Id,
                    UserId = userId,
                    Description = totalReservationDto.description,
                    Price = 0,
                    StartDate = reservationCreateDto.StartDate,
                };

            }
            if (totalreservation.Reservations != null)
            {
                foreach (var res in totalreservation.Reservations)
                {
                    if (res.Status == Status.Pending)
                    {
                        throw new InvalidOperationException("Cannot add a new reservation while there are pending reservations.");
                    }
                }
            }


            var reservation = new ReservationOfRoom
            {
                StartDate = reservationCreateDto.StartDate, // Set to current date/time


                Status = Status.Pending, // Set to a default status
                SpecialRequests = reservationCreateDto.SpecialRequests,
                Notes = reservationCreateDto.Notes,
                TotalPrice = 0, // Initialize to 0 or some default value
                PriceTillNow = 0, // Initialize to 0 or some default value
                UpdatedPriceDate = DateTime.Now, // Set to current date/time
                IsPrivate = reservationCreateDto.IsPrivate,
                RoomId = reservationCreateDto.RoomId
            };
            //checking if room is available 
            Room room = _roomRepository.GetByIdAsync(reservationCreateDto.RoomId).Result;
            if (room.CurrentCapacity == room.Capacity)
            {
                throw new InvalidOperationException("Room is not available.");
            }
            else
            {
                room.CurrentCapacity++;

                _roomRepository.Update(room);
                reservation.Rooms = room;
                _reservationRepository.AddReservation(reservation, totalreservation.Id);
                _reservationRepository.SaveAsync();
                return new ReservationReadDto
                {
                    Id = reservation.Id,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate.HasValue ? reservation.EndDate.Value : default(DateTime),
                    Status = reservation.Status,
                    Notes = reservation.Notes,
                    PriceTillNow = reservation.PriceTillNow,
                    IsPrivate = reservation.IsPrivate,
                    Rooms = new RoomReadReservationDto
                    {
                        Id = room.ID,
                        Name = room.Name,
                        Description = room.Description
                    }
                };

            }
        }
        public async Task<TotalReservationsReadDto> MakeTotalReservation(TotalReservationCreateDto totalReservationCreateDto)
        {
            var totalReservation = new TotalReservations
            {
                UserId = totalReservationCreateDto.UserId,
                Price = 0
            };
            var createdTotalReservation = await _reservationRepository.AddAsync(totalReservation);
            await _reservationRepository.SaveAsync();
            return new TotalReservationsReadDto
            {
                Id = createdTotalReservation.Id,
                description = createdTotalReservation.Description,
                totalPrice = createdTotalReservation.Price,

            };

        }
        public async Task<ReservationReadDto> UpdateReservation(int id, ReservationUpdateDto reservationUpdateDto)
        {
            DateTime timeNow = DateTime.Now;
            var totalReservation = _reservationRepository.getReservationsByid(id);
            if (totalReservation == null)
                throw new KeyNotFoundException("Reservation not found.");

            var reservation = totalReservation.Reservations.FirstOrDefault(r => r.Id == reservationUpdateDto.Id);
            if (reservation.Status == Status.Pending && reservationUpdateDto.Status == Status.Completed)//departure
            {

                if (reservation.IsPrivate)
                {



                    var room = await _roomRepository.getRoomByIdWithReservations(reservation.RoomId);//any repo must have await 
                    if (room == null)
                        throw new KeyNotFoundException("Room not found.");

                    var reservationsInRoom = room.Reservations.Where(r => r.Status == Status.Pending);//don't need paging

                    if (room.CurrentCapacity > room.MinimumPrivateCapacity)
                        foreach (var res in reservationsInRoom)
                        {
                            decimal timeSpan = (decimal)(timeNow - res.UpdatedPriceDate).TotalHours;
                            res.TotalPrice += room.PrivatePricePerPerson * (timeSpan);
                            res.UpdatedPriceDate = timeNow;
                            _reserveRepo.Update(res);
                            await _reserveRepo.SaveAsync();
                        }


                    else
                        foreach (var res in reservationsInRoom)
                        {
                            decimal timeSpan = (decimal)(DateTime.Now - res.UpdatedPriceDate).TotalHours;
                            res.TotalPrice += (room.PrivatePrice / room.CurrentCapacity) * timeSpan;
                            res.UpdatedPriceDate = timeNow;

                            _reserveRepo.Update(res);
                            await _reserveRepo.SaveAsync();
                        }




                }

                else
                {

                    var price = reservation.Rooms.SharedPricePerPerson;
                    decimal timeSpan = (decimal)(timeNow - reservation.StartDate).TotalHours;
                    reservation.TotalPrice += price * timeSpan;
                    reservation.UpdatedPriceDate = timeNow;
                    _reserveRepo.Update(reservation);
                    await _reserveRepo.SaveAsync();
                }
                reservation.EndDate = timeNow;
                reservation.Status = (Status)reservationUpdateDto.Status;


                reservation.Rooms.CurrentCapacity--;
                _roomRepository.Update(reservation.Rooms);
                await _roomRepository.SaveAsync();

            }
            else if (reservation.Status == Status.Pending && reservationUpdateDto.Status == Status.Cancelled)
            {
                reservation.Status = (Status)reservationUpdateDto.Status;
                reservation.EndDate = timeNow;
                reservation.TotalPrice = 0;
                reservation.Rooms.CurrentCapacity--;
                _roomRepository.Update(reservation.Rooms);
                await _roomRepository.SaveAsync();
            }
            else
            {
                reservation.Status = (Status)reservationUpdateDto.Status;
            }
            reservation.StartDate = (reservationUpdateDto.StartDate != null) ? reservation.StartDate : default(DateTime);
            reservation.Notes = reservationUpdateDto.Notes;
            reservation.SpecialRequests = reservationUpdateDto.SpecialRequests;
            _reserveRepo.Update(reservation);
            await _reserveRepo.SaveAsync();

            var ret = new ReservationReadDto
            {
                Id = reservation.Id,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate.HasValue ? reservation.EndDate.Value : default(DateTime),
                Status = reservation.Status,
                Notes = reservation.Notes,
                PriceTillNow = reservation.TotalPrice,
                IsPrivate = reservation.IsPrivate,
                Rooms = new RoomReadReservationDto
                {
                    Id = reservation.Rooms.ID,
                    Name = reservation.Rooms.Name,
                    Description = reservation.Rooms.Description
                }
            };
            return ret;
        }
        public async Task<bool> DeleteTotalReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return false;
            }

            await _reservationRepository.DeleteByIdAsync(id);
            await _reservationRepository.SaveAsync();
            return true;
        }
        public async Task<bool> DeleteReservationAsync(int totalReservationId, int reservationId)
        {
            var totalReservation = _reservationRepository.getReservationsByid(totalReservationId);
            if (totalReservation == null)
            {
                return false;
            }

            var reservation = totalReservation.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null)
            {
                return false;
            }

            totalReservation.Reservations.Remove(reservation);
            await _reserveRepo.DeleteByIdAsync(reservationId);
            await _reserveRepo.SaveAsync();
            return true;
        }

        public async Task<List<UpcomingReservationReadIDDto>> GetUpcomingReservationsAsync(int roomId)
        {
            var reservations = await _reserveRepo.GetUpcomingReservationsWithUserAsync(roomId);
            if (reservations == null || !reservations.Any())
            {
                return null;
            }

            return reservations.Select(r => new UpcomingReservationReadIDDto
            {
                UserName = r.TotalReservations.user.Name,
                StartDate = r.StartDate,
                EndDate = r.EndDate.HasValue ? r.EndDate.Value : default(DateTime)

            }).ToList();
        }



        public async Task<List<UpcomingReservationReadDto>> GetllUpcomingReservations()
        {
            var upcomingReservations = await _reserveRepo.GetAllUpcomingReservationsWithRooms();
            if (upcomingReservations == null || !upcomingReservations.Any())
            {
                return null;
            }


            //the paid total reservations are not included in the list of total reservations

            return upcomingReservations.Select(tr => new UpcomingReservationReadDto
            {
                Id = tr.Id,
                StartDate = tr.StartDate,
                EndDate = tr.EndDate.HasValue ? tr.EndDate.Value : default(DateTime),
                Notes = tr.Notes,
             
                Rooms = new RoomReadReservationDto
                {
                    Id = tr.Rooms.ID,
                    Name = tr.Rooms.Name,
                    Description = tr.Rooms.Description
                },
                Users = new UserUpcomingReservationReadDto
                {
                    Id = tr.TotalReservations.user.Id,
                    Name = tr.TotalReservations.user.Name,
                    Email = tr.TotalReservations.user.Email,
                    PhoneNumber = tr.TotalReservations.user.PhoneNumber,
                   
                }
            }).ToList();
        }
    }
      

}


