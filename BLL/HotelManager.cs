﻿using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class HotelManager : IHotelManager
    {

        public HotelManager(IConfiguration configuration)
        {
            HotelDb = new HotelDb(configuration);
        }

        public IHotelDb HotelDb { get; }

        
        public List<Hotel> GetAllHotels(int id)
        {
            return HotelDb.GetAllHotels(id);
        }
    }
}
