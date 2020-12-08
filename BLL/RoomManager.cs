using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RoomManager : IRoomManager
    {

        private IRoomDB RoomDB { get; }

        public RoomManager(IRoomDB roomDB)
        {
            RoomDB = roomDB;
        }

        public Room UpdatePriceRoom(Room room)
        {
            return RoomDB.UpdatePriceRoom(room);
        }

        public List<Room> SearchRoomSimple(string location, int id)
        {
            return RoomDB.SearchRoomSimple(location, id);
        }

        public List<int> SearchIdRoomSimple(string location)
        {
            return RoomDB.SearchIdRoomSimple(location);
        }

        public Room SearchRoomById(int id)
        {
            return RoomDB.SearchRoomById(id);
        }

        public List<Room> SearchEveryRooms()
        {
            return RoomDB.SearchEveryRooms();
        }

        public List<Room> GetAvailableRooms(List<Room> listRooms, List<int> listIdBooked)
        {
            if (listIdBooked.Count == 0)
            {
                return listRooms;
            }
            else
            {
                List<Room> listResults = new List<Room>();
                var roomResult = RoomDB.SearchEveryRooms();
                int sizeBooked = listIdBooked.Count;

                for (int i = 0; i < sizeBooked; i++)
                {
                    foreach (var room in roomResult)
                    {
                        int bookedRoom = listIdBooked[i];
                        if (room.IdRoom != bookedRoom)
                        {
                            listResults.Add(room);
                        }
                    }
                }
                return listResults;
            }
        }

        public List<Room> GetRoomsMultiQueries(List<Object>listCriteria,List<Room>listRooms)
        {
            List<Room> listResult = new List<Room>();
            List<Room> listResultTemp = new List<Room>();

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
            }


            if (listResult.Count.Equals(0))
            {
                listResult = listRooms;
            }

            return listResult;
        }

    }
}
