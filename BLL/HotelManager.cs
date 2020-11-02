using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BLL
{
    public class HotelManager : IHotelManager
    {

        public HotelManager(IConfiguration configuration)
        {
            HotelDb = new HotelDB(configuration);
        }

        public IHotelDB HotelDb { get; }


        public Hotel SearchHotelById(int IdHotel)
        {
            return HotelDb.SearchHotelById(IdHotel);
        }


        public List<Hotel> SearchHotelSimple(int IdHotel)
        {
            return HotelDb.SearchHotelSimple(IdHotel);
        }

         //Ancienne methode fausse/pas a jour
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

        public List<Hotel> getHotelsMultiQueries(List<Object> listCriteria, List<Hotel> listHotels)
        {
            List<Hotel> listResult = new List<Hotel>();
            List<Hotel> listResultTemp = new List<Hotel>();

            Console.WriteLine(listResult.Count);


            var location = listCriteria[0];
            var category = listCriteria[1];
            var hasWifi = listCriteria[2];
            var hasParking = listCriteria[3];


            if (location != null)
            {
                string locationV = (string)location;

                foreach (var hotel in listHotels)
                {
                    if (hotel.Location.Equals(locationV))
                    {
                        listResult.Add(hotel);
                    }
                }
            }

            Console.WriteLine(listResult.Count);

            if (category != null)
            {
                int categoryV = (int)category;

                if (listResult.Count.Equals(0))
                {
                    foreach (var hotel in listHotels)
                    {
                        if (hotel.Category.Equals(categoryV))
                        {
                            listResult.Add(hotel);
                        }
                    }
                }
                else
                {
                    foreach (var hotel in listResult)
                    {
                        if (!hotel.Category.Equals(categoryV))
                        {
                            listResultTemp.Add(hotel);
                        }
                    }
                }
            }

            if (!listResultTemp.Count.Equals(0))
            {
                listResult = listResultTemp;
                listResultTemp = new List<Hotel>();
            }

            Console.WriteLine(listResult.Count);

            if (hasWifi != null)
            {
                bool hasWifiV = (bool)hasWifi;

                if (listResult.Count.Equals(0))
                {
                    foreach (var hotel in listHotels)
                    {
                        if (hotel.HasWifi.Equals(hasWifiV))
                        {
                            listResult.Add(hotel);
                        }
                    }
                }
                else
                {
                    foreach (var hotel in listResult)
                    {
                        if (!hotel.HasWifi.Equals(hasWifiV))
                        {
                            listResultTemp.Add(hotel);
                        }
                    }
                }
            }

            if (!listResultTemp.Count.Equals(0))
            {
                listResult = listResultTemp;
                listResultTemp = new List<Hotel>();
            }


            Console.WriteLine(listResult.Count);

            if (hasParking != null)
            {
                bool hasParkingV = (bool)hasParking;

                if (listResult.Count.Equals(0))
                {
                    foreach (var hotel in listHotels)
                    {
                        if (hotel.HasParking.Equals(hasParkingV))
                        {
                            listResult.Add(hotel);
                        }
                    }
                }
                else
                {
                    foreach (var hotel in listResult)
                    {
                        if (!hotel.HasParking.Equals(hasParkingV))
                        {
                            listResultTemp.Add(hotel);
                        }
                    }
                }
            }


            if (listResult.Count.Equals(0))
            {
                listResult = listHotels;
            }

            Console.WriteLine(listResult.Count);

            return listResult;
        }
    }
}
