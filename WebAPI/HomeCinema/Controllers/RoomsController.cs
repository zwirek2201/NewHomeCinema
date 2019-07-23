using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeCinema.Data;

namespace HomeCinema.Controllers
{
    [Produces("application/json")]
    [Route("api/Rooms")]
    public class RoomsController : Controller
    {
        // GET: api/Rooms
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                List<Room> rooms = RoomsManager.GetRooms();

                return Ok(rooms);
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Rooms/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Room room = RoomsManager.GetRoom(id);

                    return Ok(room);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }

        // GET: api/Rooms/5/Seats
        [HttpGet("{id}/seats", Name = "GetSeats")]
        public IActionResult GetSeats(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<Seat> seats = SeatManager.GetRoomSeats(id);

                    return Ok(seats);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }

        }

        //// POST: api/Rooms
        //[HttpPost]
        //public IActionResult Post([FromBody]string value)
        //{

        //}

        //// PUT: api/Rooms/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody]string value)
        //{

        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{

        //}
    }
}
