using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Http.Headers;
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
            var checkIn2 = new DateTime(2020, 11, 6);
            var checkOut2 = new DateTime(2020, 11, 8);

            //bookingManager.AddBooking(new Booking { Reference = DateTime.Now.ToString("20200811123"), CheckIn = checkIn2, CheckOut = checkOut2, Lastname = "Meric", Firstname = "Chevrier", Amount = 300, IdRoom = 7 });
            bookingManager.GetEveryBookings();

            //Delete one reservation
            //bookingManager.DeleteBooking(24);


            //update Booking
            
            Console.WriteLine("Update Booking");
            var checkIn3 = new DateTime(2020, 11, 12);
            var checkOut3 = new DateTime(2020, 11, 15);
            bookingManager.UpdateBooking("20201107182400", checkIn3, checkOut3);
            bookingManager.GetEveryBookings();
            


        }
    }


}
