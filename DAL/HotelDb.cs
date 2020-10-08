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

        public HotelDb()
        {
        }

        //list of every Hotels from every city
        public List<Hotel> SearchHotels(List<string> arrayList)
        {

            var name = (string)arrayList[0];
            var location = (string)arrayList[1];
            var category = (string)arrayList[2];
            var haswifi = (string)arrayList[3];
            var hasparking = (string)arrayList[4];
            var type = (string)arrayList[5];
            var price = (string)arrayList[6];
            var hastv = (string)arrayList[7];
            var hashairdryer = (string)arrayList[8];

            Console.WriteLine("avant la cn");
            List<Hotel> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");
           
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings)) 
                {
                    //faire une show all et faire les critères dans la BLL
                    string query = "SELECT * FROM Hotel H, Room R WHERE H.Name = @H.Name AND H.Location = @H.Location AND H.Category = @H.Category "
                        + "AND H.HasWifi = @H.HasWifi AND H.HasParking = @H.HasParking AND R.Type = @R.Type AND R.Price = @R.Price "
                        + "AND R.HasTV = @R.HasTV AND R.HasHairDryer = @R.HasHairDryer"; 
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

                    Console.WriteLine("après la cn");

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
