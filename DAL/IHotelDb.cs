﻿using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IHotelDb 
    {
        IConfiguration Configuration { get; }
        List<Hotel> SearchHotelSimple(int IdHotel);
        List<Hotel> SearchHotels(List<Object> arrayList);
        List<Hotel> GetAllHotels();

    }
}
