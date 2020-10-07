using DAL;
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

        
        public List<Hotel> SearchHotels(List<Object> arrayList)
        {
            String typeValue;
            String typeNull = "IS NOT NULL";
            String type = "=";
            String app = "'";
            
            for(int i=1; i < 9; i++)
            {
                if (arrayList.IndexOf(i).Equals(null))
                {
                    type = typeNull;
                    arrayList[i]=type;
                }
                else
                {
                    typeValue = arrayList.IndexOf(i).ToString();
                    type = $"{type}{app}{typeValue}{app}";
                    arrayList[i] = type;
                }
            }

            return HotelDb.SearchHotels(arrayList);
        }
    }
}
