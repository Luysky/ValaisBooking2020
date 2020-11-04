using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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

        public List<Hotel> GetHotels()
        {
            return HotelDb.GetAllHotels();
        }

        public List<Hotel> GetHotelsMultiQueries(List<Object> listCriteria, List<Hotel> listHotels)
        {
            List<Hotel> listResult = new List<Hotel>();
            List<Hotel> listResultTemp = new List<Hotel>();

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

            return listResult;
        }
    }
}
