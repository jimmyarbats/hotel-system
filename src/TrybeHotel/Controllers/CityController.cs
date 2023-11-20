using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }
        
        // 2. Desenvolva o endpoint GET /city
        [HttpGet]
        public IActionResult GetCities(){
            return Ok(this._repository.GetCities());
        }

        // 3. Desenvolva o endpoint POST /city
        [HttpPost]
        public IActionResult PostCity([FromBody] City city){
            return CreatedAtRoute("", this._repository.AddCity(city));
        }

        // 3. Desenvolva o endpoint PUT /city
        [HttpPut]
        public IActionResult PutCity([FromBody] City city){
            try
            {
                var cityToUpdate = _repository.UpdateCity(city);

                cityToUpdate.name = city.Name;
                cityToUpdate.state = city.State;

                return Ok(cityToUpdate);
            }

            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}