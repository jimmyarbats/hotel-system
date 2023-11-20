using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var db = this._context.Rooms;
            var rooms = db
                .Where(room => room.HotelId == HotelId)
                .Select(room => new RoomDto
                {
                    roomId = room.RoomId,
                    name = room.Name,
                    capacity = room.Capacity,
                    image = room.Image,
                    hotel = new HotelDto
                    {
                        hotelId = room.Hotel.HotelId,
                        name = room.Hotel.Name,
                        address = room.Hotel.Address,
                        cityId = room.Hotel.CityId,
                        cityName = room.Hotel.City.Name,
                        state = room.Hotel.City.State,
                    }
                });

            return rooms.ToList(); 
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            this._context.Rooms.Add(room);
            this._context.SaveChanges();
            Room addedRoom = this._context.Rooms
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.City)
                .First(r => r.Name == room.Name);
            RoomDto newRoom = new RoomDto
            {
                roomId = addedRoom.RoomId,
                name = addedRoom.Name,
                capacity = addedRoom.Capacity,
                image = addedRoom.Image,
                hotel = new HotelDto
                {
                    hotelId = addedRoom.Hotel.HotelId,
                    name = addedRoom.Hotel.Name,
                    address = addedRoom.Hotel.Address,
                    cityId = addedRoom.Hotel.CityId,
                    cityName = addedRoom.Hotel.City.Name,
                    state = addedRoom.Hotel.City.State,
                }
            };

            return newRoom; 
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId) {
            var room = this._context.Rooms.Find(RoomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
        }
    }
}