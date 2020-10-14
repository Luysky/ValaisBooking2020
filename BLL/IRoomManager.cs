using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IRoomManager
    {
        IRoomDB RoomDB { get; }
        Room UpdatePriceRoom(Room room);
    }
}
