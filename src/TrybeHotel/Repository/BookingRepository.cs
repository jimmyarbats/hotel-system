using TrybeHotel.Models;
using TrybeHotel.Dto;
using Azure;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);

            if (room == null)
            {
                throw new Exception("Room not found");
            }

            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var newBooking = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                UserId = user.UserId,
                RoomId = room.RoomId,
            };

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            return new BookingResponse
            {
                bookingId = newBooking.BookingId,
                checkIn = newBooking.CheckIn,
                checkOut = newBooking.CheckOut,
                guestQuant = newBooking.GuestQuant,
                room = new RoomDto
                {
                    roomId = room.RoomId,
                    name = room.Name,
                    capacity = room.Capacity,
                    image = room.Image,
                    hotel = new HotelDto
                    {
                        hotelId = hotel.HotelId,
                        name = hotel.Name,
                        address = hotel.Address,
                        cityId = hotel.CityId,
                        cityName = city?.Name,
                        state = city.State,
                    }
                }
            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var get = _context.Bookings
                        .Where(b => b.UserId == user.UserId && b.BookingId == bookingId)
                        .Select(b => new BookingResponse
                        {
                            bookingId = b.BookingId,
                            checkIn = b.CheckIn,
                            checkOut = b.CheckOut,
                            guestQuant = b.GuestQuant,
                            room = new RoomDto
                            {
                                roomId = b.Room!.RoomId,
                                name = b.Room!.Name,
                                capacity = b.Room!.Capacity,
                                image = b.Room!.Image,
                                hotel = new HotelDto
                                {
                                    hotelId = b.Room.Hotel!.HotelId,
                                    name = b.Room.Hotel!.Name,
                                    address = b.Room.Hotel!.Address,
                                    cityId = b.Room.Hotel!.CityId,
                                    cityName = b.Room.Hotel!.Name,
                                    state = b.Room.Hotel.City!.State
                                }
                            }
                        }).FirstOrDefault();

            if (get == null)
            {
                throw new Exception("Booking not found");
            }

            return get;
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}