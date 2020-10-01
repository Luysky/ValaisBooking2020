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

            /*
             * cmd.Parameters.AddWithValue("@H.Name", name);
                    cmd.Parameters.AddWithValue("@H.Location", location);
                    cmd.Parameters.AddWithValue("@H.Category", category);
                    cmd.Parameters.AddWithValue("@H.HasWifi", haswifi);
                    cmd.Parameters.AddWithValue("@H.HasParking", hasparking);
                    cmd.Parameters.AddWithValue("@R.Type", type);
                    cmd.Parameters.AddWithValue("@R.Price", price);
                    cmd.Parameters.AddWithValue("@R.HasTV", hastv);
                    cmd.Parameters.AddWithValue("@R.HasHairDryer", hashairdryer);
             */
            Console.WriteLine("Recherche en cours");
            Console.WriteLine(test.SearchHotels(list));
        }
    }
}
