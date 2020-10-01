using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class HotelDb : IHotelDb
    {
        public IConfiguration Configuration { get; }

        public HotelDb(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //list of every Hotels from every city
        public List<Hotel> SearchHotels(List<Object> arrayList)
        {

            var name = arrayList.IndexOf(1);
            var location = arrayList.IndexOf(2);
            var category = arrayList.IndexOf(3);
            var haswifi = arrayList.IndexOf(4);
            var hasparking = arrayList.IndexOf(5);
            var type = arrayList.IndexOf(6);
            var price = arrayList.IndexOf(7);
            var hastv = arrayList.IndexOf(8);
            var hashairdryer = arrayList.IndexOf(9);

            List<Hotel> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings)) 
                {
                    string query = "SELECT * FROM Hotel H, Room R WHERE H.Name IS NOT NULL AND H.Location IS NOT NULL AND H.Category IS NOT NULL "
                        + "AND H.HasWifi IS NOT NULL AND H.HasParking IS NOT NULL AND R.Type IS NOT NULL AND R.Price IS NOT NULL "
                        + "AND R.HasTV IS NOT NULL AND R.HasHairDryer IS NOT NULL";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@H.Name", name);
                    cmd.Parameters.AddWithValue("@H.Location", location);
                    cmd.Parameters.AddWithValue("@H.Category", category);
                    cmd.Parameters.AddWithValue("@H.HasWifi", haswifi);
                    cmd.Parameters.AddWithValue("@H.HasParking", hasparking);
                    cmd.Parameters.AddWithValue("@R.Type", type);
                    cmd.Parameters.AddWithValue("@R.Price", price);
                    cmd.Parameters.AddWithValue("@R.HasTV", hastv);
                    cmd.Parameters.AddWithValue("@R.HasHairDryer", hashairdryer);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Hotel>();

                            Hotel hotel = new Hotel();

                            hotel.IdHotel = (int)dr["IdHotel"];
                            hotel.Name = (string)dr["Name"];
                            hotel.Description = (string)dr["Description"];
                            hotel.Location = (string)dr["Location"];
                            hotel.Category = (int)dr["Category"];
                            hotel.HasWifi = (bool)dr["HasWifi"];
                            hotel.HasParking = (bool)dr["HasParking"];
                            hotel.Email = (string)dr["Email"];
                            hotel.Website = (string)dr["Website"];
                            results.Add(hotel);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return results;
        }
    }
}
