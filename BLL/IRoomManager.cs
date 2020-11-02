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
        List<Room> SearchRoomSimple(String location);
        List<int> SearchIdRoomSimple(String location);
        List<Room> SearchEveryRooms();
        List<Room> getRoomsMultiQueries(List<Object> listCriteria, List<Room> listRooms);
        void getSearchedHotel(RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, string city);
    }
}
