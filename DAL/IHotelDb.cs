using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DAL
{
    public interface IHotelDB 
    {
        IConfiguration Configuration { get; }
        List<Hotel> SearchHotelSimple(int IdHotel);
        List<Hotel> GetAllHotels();
        Hotel SearchHotelById(int IdHotel);

    }
}
