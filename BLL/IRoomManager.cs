using DTO;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IRoomManager
    {
        IRoomDB RoomDB { get; }
        Room UpdatePriceRoom(Room room);
        List<Room> SearchRoomSimple(string location);
        List<int> SearchIdRoomSimple(string location);
        List<Room> SearchEveryRooms();
        List<Room> GetRoomsMultiQueries(List<Object> listCriteria, List<Room> listRooms);
        void GetSearchedHotel(RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, string city);
    }
}
