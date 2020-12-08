using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IHotelManager
    {
        IHotelDB HotelDb { get; }
        Hotel SearchHotelById(int IdHotel);
        List<Hotel> SearchListHotelById(int IdHotel);
        List<Hotel> GetHotels();
        List<Hotel> GetHotelsMultiQueries(List<Object> listCriteria, List<Hotel> hotel);
        List<int> GetIdRoomFromBookingList(List<Booking> listBooking, DateTime checkin, DateTime checkout);
        List<int> GetHotelFromRoomId(List<int> listId);
        double GetExtraPrice(double price, int totalRooms, int totalAvailableRooms);

    }
}
