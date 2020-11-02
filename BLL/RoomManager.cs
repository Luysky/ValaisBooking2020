using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace BLL
{
    public class RoomManager : IRoomManager
    {

        public RoomManager(IConfiguration configuration)
        {
            RoomDB = new RoomDB(configuration);
        }

        public IRoomDB RoomDB { get; }

        public Room UpdatePriceRoom(Room room)
        {
            return RoomDB.UpdatePriceRoom(room);
        }

        public List<Room> SearchRoomSimple(string location)
        {
            return RoomDB.SearchRoomSimple(location);
        }

        public List<int> SearchIdRoomSimple(string location)
        {
            return RoomDB.SearchIdRoomSimple(location);
        }

        public List<Room> SearchEveryRooms()
        {
            return RoomDB.SearchEveryRooms();
        }


        public void getSearchedHotel(RoomManager roomDBManager,HotelManager hotelDBManager,PictureManager pictureDBManager,string city)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get searched Hotel--");
            var roomIdResult = roomDBManager.SearchRoomSimple(city);

            foreach (var room in roomIdResult)
            {

                Console.WriteLine(room.ShortInfo());

                Console.WriteLine();
                var hotelResult = hotelDBManager.SearchHotelSimple(room.IdHotel);
                foreach (var hotel in hotelResult)
                {
                    Console.WriteLine(hotel.ShortInfo());
                    Console.WriteLine();

                    var pictureResult = pictureDBManager.SearchListPicture(room.IdRoom);
                    Console.WriteLine("Pictures : ");
                    foreach (var picture in pictureResult)
                    {
                        Console.WriteLine(picture.Url);
                    }

                    Console.WriteLine();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine();
                }
            }
        }

        public List<Room> getRoomsMultiQueries(List<Object>listCriteria,List<Room>listRooms)
        {
            List<Room> listResult = new List<Room>();
            List<Room> listResultTemp = new List<Room>();

            Console.WriteLine(listResult.Count);


            var type = listCriteria[0];
            var hasTv = listCriteria[1];
            var hasHairDryer = listCriteria[2];


            if (type != null)
            {
                int typeV = (int) type;

                foreach(var room in listRooms)
                {
                    if (room.Type.Equals(typeV))
                    {
                        listResult.Add(room);
                    }
                }
            }

            Console.WriteLine(listResult.Count);

            if (hasTv != null)
            {
                bool hasTvV = (bool)hasTv;

                if (listResult.Count.Equals(0))
                {
                    foreach (var room in listRooms)
                    {
                        if (room.HasTV.Equals(hasTvV))
                        {
                            listResult.Add(room);
                        }
                    }
                }
                else
                {
                    foreach (var room in listResult)
                    {
                        if (!room.HasTV.Equals(hasTvV))
                        {
                            listResultTemp.Add(room);
                        }
                    }
                }              
            }

            if (!listResultTemp.Count.Equals(0)) 
            { 
                listResult = listResultTemp;
                listResultTemp = new List<Room>();
            }

            Console.WriteLine(listResult.Count);

            if (hasHairDryer != null)
            {
                bool hasHairDryerV = (bool)hasHairDryer;

                if (listResult.Count.Equals(0))
                {
                    foreach (var room in listRooms)
                    {
                        if (room.HasTV.Equals(hasHairDryerV))
                        {
                            listResult.Add(room);
                        }
                    }
                }
                else
                {
                    foreach (var room in listResult)
                    {
                        if (!room.HasTV.Equals(hasHairDryerV))
                        {
                            listResultTemp.Add(room);
                        }
                    }
                }
            }

            if (!listResultTemp.Count.Equals(0))
            {
                listResult = listResultTemp;
                listResultTemp = new List<Room>();
            }

            Console.WriteLine(listResult.Count);


            if (listResult.Count.Equals(0))
            {
                listResult = listRooms;
            }

            Console.WriteLine(listResult.Count);

            return listResult;
        }

    }
}
