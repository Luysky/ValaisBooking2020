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
            
            var hotelDBManager = new HotelManager(Configuration);
            var roomDBManager = new RoomManager(Configuration);
            var pictureDBManager = new PictureManager(Configuration);
            var bookingsDBManager = new BookingManager(Configuration);

            //Affiche tous les hotels se trouvant dans une localité
            //roomDBManager.getSearchedHotel(roomDBManager,hotelDBManager,pictureDBManager,"Martigny");

            //Afficher toutes les réservations
            //bookingsDBManager.getEveryBookings(bookingsDBManager);

            //Afficher toutes les réservations sur une chambre pour une période donnée
            var checkIn = new DateTime(2020, 10, 10);
            var checkOut = new DateTime(2020, 11, 15);
            //bookingsDBManager.getBookingsWithRoomAndDates(bookingsDBManager, 1, checkIn, checkOut);

            //Afficher la requête search
            //bookingsDBManager.searchSimple(roomDBManager, bookingsDBManager.getBookingsWithRoomAndDates(bookingsDBManager, 1, checkIn, checkOut), hotelDBManager, pictureDBManager, "Martigny");





            //Récupration de toutes les rooms réservées pour une période précise
            //Retourne une liste de int (numéro IdRoom)
            var bookingsResult = bookingsDBManager.GetAllReservationDateSimple(checkIn, checkOut);

            //Retourne une liste de room
            var listRoomAvailable = bookingsDBManager.searchEveryAvailableRooms(roomDBManager, hotelDBManager, pictureDBManager, bookingsResult);

            List<Object> criteriaRoom = new List<object>();
            criteriaRoom.Add(1);
            criteriaRoom.Add(true);
            criteriaRoom.Add(true);

            /*
             critera.Add(2);
            critera.Add(true);
            critera.Add(false);
            */

            //Ca me ressort une liste de rooms qui correspondent aux conditions
            //roomDBManager.getRoomsMultiQueries(criteriaRoom,listRoomAvailable);

            //Retourne une liste d'hotels - Tous les hotels
            //var hotelsResult = hotelDBManager.GetHotels();

            List<Object> criteriaHotel = new List<object>();
            criteriaHotel.Add("Martigny");
            criteriaHotel.Add(null);
            criteriaHotel.Add(true);
            criteriaHotel.Add(false);

            //Retourne une liste d hotels qui correspondent aux conditions 
            //var listHotelAvailable = hotelDBManager.getHotelsMultiQueries(criteriaHotel,hotelsResult);

            //METHODE FAUSSE ! A CORRIGER LES DOUBLONS DANS LE RESULTAT
            bookingsDBManager.searchAdvanced(bookingsDBManager, roomDBManager, hotelDBManager, pictureDBManager, criteriaRoom, criteriaHotel,checkIn,checkOut);



            /*
                

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
