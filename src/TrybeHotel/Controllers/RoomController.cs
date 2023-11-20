using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        [HttpGet("{HotelId}")]
        public IActionResult GetRoom(int HotelId){
            return Ok(this._repository.GetRooms(HotelId));
        }

        // 7. Desenvolva o endpoint POST /room
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult PostRoom([FromBody] Room room){
            return CreatedAtRoute("", this._repository.AddRoom(room));
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        [HttpDelete("{RoomId}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int RoomId)
        {
            this._repository.DeleteRoom(RoomId);
            return NoContent();
        }
    }
}