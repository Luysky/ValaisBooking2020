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
        public List<Hotel> GetAllHotels(List<Object> arrayList)
        {

            String typeArray;
            String typeNull = "*";
            String type;

            if (arrayList.IndexOf(1).Equals(null))
            {
                type = typeNull;
            }
            else
            {
                typeArray = arrayList.IndexOf(1).ToString();
                type = typeArray;
            }

            //int type = Convert.ToInt32(arrayList.IndexOf(1));
            


            List<Hotel> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings)) 
                {
                    string query = "SELECT * FROM Hotel";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

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
