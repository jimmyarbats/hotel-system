using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 2. Desenvolva o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            var db = this._context.Cities;
            var cities = db.Select(city => new CityDto
            {
                cityId = city.CityId,
                name = city.Name,
                state = city.State,
            });

            return cities.ToList();
        }

        // 3. Desenvolva o endpoint POST /city
        public CityDto AddCity(City city)
        {
            this._context.Cities.Add(city);
            this._context.SaveChanges();

            City addedCity = this._context.Cities.First(c => c.Name == city.Name);
            CityDto newCity = new CityDto
            {
                cityId = addedCity.CityId,
                name = addedCity.Name,
                state = city.State,
            };

            return newCity;
        }

        public CityDto UpdateCity(City city)
        {
            var cityToUpdate = _context.Cities.FirstOrDefault(c => c.CityId == city.CityId);

            if (cityToUpdate == null)
            {
                throw new Exception("City not found");
            }

            cityToUpdate.Name = city.Name;
            cityToUpdate.State = city.State;

            _context.SaveChanges();

            return new CityDto
            {
                cityId = cityToUpdate.CityId,
                name = cityToUpdate.Name,
                state = cityToUpdate.State,
            };
        }
    }
}