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
        List<Hotel> SearchHotelSimple(int IdHotel);

        List<Hotel> SearchHotels(List<Object> arrayList);

        List<Hotel> GetHotels();
        List<Hotel> getHotelsMultiQueries(List<Object> listCriteria, List<Hotel> listHotels);


    }
}
