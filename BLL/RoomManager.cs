using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RoomManager : IRoomManager
    {
        public RoomManager(IConfiguration configuration)
        {
            RoomDB = new RoomDB(configuration);
        }

        public IRoomDB RoomDB { get; }

        public Room UpdatePriceRoom(Room room)
        {
            return RoomDB.UpdatePriceRoom(room);
        }
    }
}
