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
            String typeArray;
            String typeNull = "IS NOT NULL";
            String type = "=";
            String type2 = "";

            for(int i=1; i < 9; i++)
            {
                if (arrayList.IndexOf(1).Equals(null))
                {
                    type = typeNull;
                }
                else
                {
                    typeArray = arrayList.IndexOf(1).ToString();
                    type = $"{type2}{typeArray}";
                }
            }

         

            //int type = Convert.ToInt32(arrayList.IndexOf(1));


            return HotelDb.SearchHotels(arrayList);
        }
    }
}
