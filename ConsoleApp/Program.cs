using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using BLL;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();
        static void Main(string[] args)
        {
            
            var hotelDbManager = new HotelManager(Configuration);
            var roomDbManager = new RoomManager(Configuration);
            var roomIdManager = new RoomManager(Configuration);

            
            Console.WriteLine("--Get all Hotels--");
            var hotels = hotelDbManager.GetHotels();
            foreach (var hotel in hotels)
            {
                //Console.WriteLine(hotel.ToString());
            }
            
           

            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get searched Rooms--");
            var roomResult = roomDbManager.SearchRoomSimple("Martigny");
            foreach (var room in roomResult)
            {
               // Console.WriteLine(room.ToString());
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get searched Hotel--");
            var roomIdResult = roomIdManager.SearchRoomSimple("Martigny");          
            int size = roomIdResult.Count;
            
            //int i = 0;
            //while (i < size)
            
                foreach (var room in roomIdResult)
                {


                    Console.WriteLine(room.ToString());
                    var hotelResult = hotelDbManager.SearchHotelSimple(room.IdHotel);
                    foreach (var hotel in hotelResult)
                    {
                        Console.WriteLine(hotel.ToString());
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("------------------------------");
                }
                }
                //i++;
            



            //test
            /*
            String one = "hello";
            String two = "'";
            String three = $"{two}{one}'";
            Console.WriteLine(three);

            HotelManager test = new HotelManager(Configuration);
            List<Object> list = new List<Object>();

            list.Add(null);
            list.Add("Sion");
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add("TRUE");
            list.Add(null);
           
            Console.WriteLine("Recherche en cours");
            Console.WriteLine(test.SearchHotels(list));
            */


        }
    }
}
