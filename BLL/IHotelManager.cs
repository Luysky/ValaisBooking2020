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
        List<Hotel> GetHotelsMultiQueries(List<Object> listCriteria, List<Hotel> listHotels);


    }
}
