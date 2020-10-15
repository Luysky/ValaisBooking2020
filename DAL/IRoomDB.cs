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
        List<Room> SearchRoomSimple(String location);
        List<int> SearchIdRoomSimple(String location);
        
    }
}
