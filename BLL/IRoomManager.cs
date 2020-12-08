using DTO;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IRoomManager
    {
        //IRoomDB RoomDB { get; }
        Room UpdatePriceRoom(Room room);
        List<Room> SearchRoomSimple(string location, int id);
        List<int> SearchIdRoomSimple(string location);
        Room SearchRoomById(int id);
        List<Room> SearchEveryRooms();
        List<Room> GetAvailableRooms(List<Room> listRooms, List<int> listIdBooked);
        List<Room> GetRoomsMultiQueries(List<Object> listCriteria, List<Room> listRooms);
    }
}
