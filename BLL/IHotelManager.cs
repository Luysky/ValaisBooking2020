using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IHotelManager
    {
        IHotelDb HotelDb { get; }

        List<Room> SearchRoomSimple(string location);

        List<Hotel> SearchHotels(List<Object> arrayList);

        List<Hotel> GetHotels();
        List<Room> GetAllRooms();
    }
}
