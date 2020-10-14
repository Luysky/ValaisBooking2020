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


            Console.WriteLine("--Get all Hotels--");
            var hotels = hotelDbManager.GetHotels();
            foreach (var hotel in hotels)
            {
                Console.WriteLine(hotel.ToString());
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get all Rooms--");
            var rooms = hotelDbManager.GetAllRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine(room.ToString());
            }
            /*
            String one = "hello";
            String two = "'";
            String three = $"{two}{one}'";
            Console.WriteLine(three);

            HotelManager test = new HotelManager(Configuration);
            List<String> list = new List<String>();

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
