using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
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


        public List<Room> SearchRoomSimple(string location)
        {
            return HotelDb.SearchRoomSimple(location);
        }


        public List<Hotel> SearchHotels(List<Object> arrayList)
        {
            String typeValue;
            //String typeNull = "IS NOT NULL";
            String type = "=";
            String typeFull = "=";
            String equal = "=";
            String app = "'";

            List<string> tabledb = new List<string>();
            tabledb.Add("H.Name");
            tabledb.Add("H.Location");
            tabledb.Add("H.Category");
            tabledb.Add("H.HasWifi");
            tabledb.Add("H.HasParking");
            tabledb.Add("H.Type");
            tabledb.Add("H.Price");
            tabledb.Add("H.HasTV");
            tabledb.Add("H.HasHairDryer");

            for (int i=0; i < 9; i++)
            {
                
                //if (arrayList[i].Equals(null))
                if (arrayList[i]==null)
                {
                    type = tabledb[i];
                    arrayList[i]=type;
                }
                else
                {
                    //typeValue = arrayList[i];
                    //typeFull = $"{equal}{app}{typeValue}{app}";
                    arrayList[i] = typeFull;
                }
            }

            return HotelDb.SearchHotels(arrayList);
        }

        public object GetHotel()
        {
            throw new NotImplementedException();
        }

        public List<Hotel> GetHotels()
        {
            return HotelDb.GetAllHotels();
        }

        public List<Room> GetAllRooms()
        {
            return HotelDb.GetAllRooms();
        }
    }
}
