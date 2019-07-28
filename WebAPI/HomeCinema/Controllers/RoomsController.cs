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
        private IRoomsManager _roomsManager;
        private ISeatsManager _seatsManager;

        public RoomsController(IRoomsManager roomsManager, ISeatsManager seatsManager)
        {
            _roomsManager = roomsManager;
            _seatsManager = seatsManager;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Room> rooms = await _roomsManager.GetRooms();

                return Ok(rooms);
            }
            catch (CouldNotPerformOperationException ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Rooms/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Room room = await _roomsManager.GetRoom(id);

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
        public async Task<IActionResult> GetSeats(int id)
        {
            try
            {
                if (id > 0)
                {
                    List<Seat> seats = await _seatsManager.GetRoomSeats(id);

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
    }
}
