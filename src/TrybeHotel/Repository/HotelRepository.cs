using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            var db = this._context.Hotels;
            var hotels = db.Select(hotel => new HotelDto
            {
                hotelId = hotel.HotelId,
                name = hotel.Name,
                address = hotel.Address,
                cityId = hotel.CityId,
                cityName = hotel.City.Name,
                state = hotel.City.State,
            });

            return hotels.ToList();
        }
        
        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            this._context.Hotels.Add(hotel);
            this._context.SaveChanges();
            
            Hotel addedHotel = this._context.Hotels
                .Include(h => h.City)
                .First(h => h.Name == hotel.Name);
            HotelDto newHotel = new HotelDto
            {
                hotelId = addedHotel.HotelId,
                name = addedHotel.Name,
                address = addedHotel.Address,
                cityId = addedHotel.City.CityId,
                cityName = addedHotel.City.Name,
                state = addedHotel.City.State,
            };

            return newHotel;
        }
    }
}