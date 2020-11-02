using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IRoomDB
    {
        IConfiguration Configuration { get; }
        Room UpdatePriceRoom(Room room);
        List<Room> SearchRoomSimple(string location);
        List<Room> SearchEveryRooms();
        List<int> SearchIdRoomSimple(string location);
        
    }
}
