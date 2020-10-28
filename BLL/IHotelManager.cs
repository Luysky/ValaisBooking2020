using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IHotelManager
    {
        IHotelDB HotelDb { get; }

        List<Hotel> SearchHotelSimple(int IdHotel);

        List<Hotel> SearchHotels(List<Object> arrayList);

        List<Hotel> GetHotels();
    
    }
}
