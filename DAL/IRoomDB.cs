using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DAL
{
    public interface IRoomDB
    {
        IConfiguration Configuration { get; }
        Room UpdatePriceRoom(Room room);
        List<Room> SearchRoomSimple(string location, int id);
        Room SearchRoomById(int id);
        List<Room> SearchEveryRooms();
        List<int> SearchIdRoomSimple(string location);
        
    }
}
