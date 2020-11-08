using System;
using System.Collections.Generic;
using System.IO;
using BLL;
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
            
            var bookingManager = new BookingManager(Configuration);


            //Afficher toutes les réservations
            //bookingManager.GetEveryBookings();


            var checkIn = new DateTime(2020, 10, 10);
            var checkOut = new DateTime(2020, 11, 15);

            //Afficher la requête search simple
            //bookingManager.SearchSimple(bookingManager.GetBookingsWithRoomAndDates(1, checkIn, checkOut), "Martigny");


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
            //bookingManager.SearchAdvanced(criteriaRoom, criteriaHotel,checkIn,checkOut);

            //Insert a new reservation
            var checkIn2 = new DateTime(2020, 11, 4);
            var checkOut2 = new DateTime(2020, 11, 7);

            
            
            //bookingManager.AddBooking(new Booking { Reference = DateTime.Now.ToString("yyyyMMddHHmmss"), CheckIn = checkIn2, CheckOut = checkOut2, Lastname = "Johansson", Firstname = "Scarlett", Amount = 300, IdRoom = 5 });
            bookingManager.GetEveryBookings();

            //Delete one reservation
            //bookingManager.DeleteBooking(24);

        }
    }


}
