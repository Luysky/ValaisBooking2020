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
            var bookingsDBManager = new BookingManager(Configuration);

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


            //Afficher toutes les réservations
            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get every bookings--");
            var bookingsResult =  bookingsDBManager.GetAllReservation();
            foreach (var booking in bookingsResult)
            {
                Console.WriteLine(booking.ShortInfo());
            }


            //Afficher toutes les réservations sur une chambre pour une période donnée
            //Permet de vérifier si la chambre est réservable ou pas. 
            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get every bookings with a date for a specific room--");
            var checkIn = new DateTime(2020, 10, 10);
            var checkOut = new DateTime(2020, 11, 15);
            bookingsResult = bookingsDBManager.GetAllReservationDate(1, checkIn, checkOut);
            
            List<int> listRoomBooked = new List<int>();
            foreach (var booking in bookingsResult)
            {     
                    Console.WriteLine(booking.ShortInfo());
                    listRoomBooked.Add(booking.IdRoom);
            }



            Console.WriteLine("------------------------------");
            Console.WriteLine("------------------------------");
            Console.WriteLine("------------------------------");

            Console.WriteLine("--Search simple--");
            var roomResult = roomDBManager.SearchRoomSimple("Martigny");


            //Comparaison de toutes les rooms et des Id déjà réservé
            //Les chambres aux idRoom non booké sont ajoutés à une nouvelle liste (liste à jour avec localisation et date)
            //Cette nouvelle liste sera utilisée pour la suite des recherches

            //Le test fonctionne car les chambres déjà bookées pour les dates en question genre la chambre 1001 IdRoom 1 n'apparait pas dans la liste

            int sizeBooked = listRoomBooked.Count;
            List<Room> listFinal = new List<Room>();

            for (int i = 0; i<sizeBooked; i++)
            {
                foreach(var room in roomResult)
                {
                    int bookedRoom = listRoomBooked[i];
                    if (room.IdRoom != bookedRoom)
                    {
                        listFinal.Add(room);
                    }
                }
            }
            foreach (var room in listFinal)
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



            //ANCIEN BROUILLON POUR SEARCH MULTIPLE
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
