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

        
        public List<Hotel> SearchHotels(List<String> arrayList)
        {
            String typeValue;
            String typeNull = "IS NOT NULL";
            String type = "=";
            String typeFull = "=";
            String equal = "=";
            String app = "'";


            for (int i=0; i < 9; i++)
            {
                
                //if (arrayList[i].Equals(null))
                if (arrayList[i]==null)
                {
                    type = typeNull;
                    arrayList[i]=type;
                }
                else
                {
                    typeValue = arrayList[i];
                    typeFull = $"{equal}{app}{typeValue}{app}";
                    arrayList[i] = typeFull;
                }
            }

            return HotelDb.SearchHotels(arrayList);
        }
    }
}
