using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class HotelDB : IHotelDB
    {
        public IConfiguration Configuration { get; }

        public HotelDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Hotel> SearchListHotelById(int IdHotel)
        {
            List<Hotel> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query ="SELECT * FROM Hotel WHERE IdHotel = @IdHotel";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdHotel", IdHotel);

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

                            if (dr["Phone"] != null)
                                hotel.Phone = (string)dr["Phone"];
                            if (dr["Email"] != null)
                                hotel.Email = (string)dr["Email"];
                            if (dr["Website"] != null)
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

        public Hotel SearchHotelById(int IdHotel)
        {
            Hotel results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Hotel WHERE IdHotel = @IdHotel";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdHotel", IdHotel);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            if (results == null)
                                results = new Hotel();

                            Hotel hotel = new Hotel();

                            hotel.IdHotel = (int)dr["IdHotel"];
                            hotel.Name = (string)dr["Name"];
                            hotel.Description = (string)dr["Description"];
                            hotel.Location = (string)dr["Location"];
                            hotel.Category = (int)dr["Category"];
                            hotel.HasWifi = (bool)dr["HasWifi"];
                            hotel.HasParking = (bool)dr["HasParking"];

                            if (dr["Phone"] != null)
                                hotel.Phone = (string)dr["Phone"];
                            if (dr["Email"] != null)
                                hotel.Email = (string)dr["Email"];
                            if (dr["Website"] != null)
                                hotel.Website = (string)dr["Website"];

                            results = hotel;

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

        public List<Hotel> GetAllHotels()
        {
            List<Hotel> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * from Hotel";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Hotel>();

                            Hotel hotel = new Hotel();
                                               
                            hotel.IdHotel = (int)dr["IdHotel"];
                            hotel.Name = (String)dr["Name"];
                            hotel.Description = (String)dr["Description"];
                            hotel.Location = (String)dr["Location"];
                            hotel.Category = (int)dr["Category"];
                            hotel.HasWifi = (bool)dr["HasWifi"];
                            hotel.HasParking = (bool)dr["HasParking"];

                            if (dr["Phone"] != null)
                                hotel.Phone = (string)dr["Phone"];
                            if (dr["Email"] != null)
                                hotel.Email = (string)dr["Email"];
                            if (dr["Website"] != null)
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
