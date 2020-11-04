using System;
using System.Collections.Generic;
using System.IO;
using BLL;
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
            
            var hotelDBManager = new HotelManager(Configuration);
            var roomDBManager = new RoomManager(Configuration);
            var pictureDBManager = new PictureManager(Configuration);
            var bookingsDBManager = new BookingManager(Configuration);


            //Afficher toutes les réservations
            //bookingsDBManager.GetEveryBookings(bookingsDBManager);


            var checkIn = new DateTime(2020, 10, 10);
            var checkOut = new DateTime(2020, 11, 15);

            //Afficher la requête search simple
            bookingsDBManager.SearchSimple(roomDBManager, bookingsDBManager.GetBookingsWithRoomAndDates(bookingsDBManager, 1, checkIn, checkOut), hotelDBManager, pictureDBManager, "Martigny");


            //Données nécessaires pour la recherche Advanced
            List<Object> criteriaRoom = new List<object>();
            criteriaRoom.Add(1);
            criteriaRoom.Add(true);
            criteriaRoom.Add(true);

            List<Object> criteriaHotel = new List<object>();
            criteriaHotel.Add("Martigny");
            criteriaHotel.Add(null);
            criteriaHotel.Add(true);
            criteriaHotel.Add(false);

            //Afficher la requête search advanced
            bookingsDBManager.SearchAdvanced(bookingsDBManager, roomDBManager, hotelDBManager, pictureDBManager, criteriaRoom, criteriaHotel,checkIn,checkOut);


        }
    }


}
