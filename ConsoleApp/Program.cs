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
            var roomDBManager = new RoomManager(Configuration);
            var pictureDBManager = new PictureManager(Configuration);

            

            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get searched Hotel--");
            var roomIdResult = roomDBManager.SearchRoomSimple("Martigny");          
            
                foreach (var room in roomIdResult)
                {

                    Console.WriteLine(room.ShortInfo());

                    Console.WriteLine();
                    var hotelResult = hotelDbManager.SearchHotelSimple(room.IdHotel);
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
