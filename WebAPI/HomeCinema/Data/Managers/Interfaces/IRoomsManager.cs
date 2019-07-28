using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public interface IRoomsManager
    {
        Task<List<Room>> GetRooms();

        Task<Room> GetRoom(int id);
    }
}
