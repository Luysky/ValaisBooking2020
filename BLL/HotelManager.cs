using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class HotelManager : IHotelManager
    {
        private IRoomManager RoomManager;

        public HotelManager(IHotelDB hotelDB, IRoomManager roomManager)
        {
            HotelDb = hotelDB;
            RoomManager = roomManager;
        }

        public IHotelDB HotelDb { get; }


        public Hotel SearchHotelById(int IdHotel)
        {
            return HotelDb.SearchHotelById(IdHotel);
        }


        public List<Hotel> SearchListHotelById(int IdHotel)
        {
            return HotelDb.SearchListHotelById(IdHotel);
        }

        public List<Hotel> GetHotels()
        {
            return HotelDb.GetAllHotels();
        }

        public List<int> GetIdRoomFromBookingList(List<Booking> listBooking, DateTime checkin, DateTime checkout)
        {
            List<int> result = new List<int>();

            
            foreach(var booking in listBooking)
            {
                if(booking.CheckIn == checkin && booking.CheckOut == checkout)
                {
                    result.Add(booking.IdRoom);
                }
                
            }
            return result;
        }

        
        public List<int> GetHotelFromRoomId(List<int> listId)
        {
            var listEveryRoom = RoomManager.SearchEveryRooms();
            List<int> result = new List<int>();

            foreach(var room in listEveryRoom)
            {
                for(int i = 0; i<listId.Count; i++)
                {
                    if(room.IdRoom == listId[i])
                    {
                        result.Add(room.IdHotel);
                    }
                }
            }
            return result;
        }

        public double GetExtraPrice(double price, int totalRooms, int totalAvailableRooms)
        {

            double finalPrice = price;
            int cpt = 0;
            double majoration = 0.0;

            double poucentage = (totalAvailableRooms * 100) / totalRooms;

            if (poucentage <= 30)
            {
                majoration = (price * 20) / 100;
                finalPrice += majoration;
                return finalPrice;
            }
            else
            {
                return price;
            }
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
                        if (hotel.Category.Equals(categoryV))
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
