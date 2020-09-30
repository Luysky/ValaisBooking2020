using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IHotelManager
    {
        IHotelDb HotelDb { get; }

        List<Hotel> GetALlHotels(int id);

    }
}
