﻿using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class RoomDB : IRoomDB
    {
        public IConfiguration Configuration { get; }

        public RoomDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Room UpdatePriceRoom(Room room)
        {
            int result = 0;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Room SET Price = @Price WHERE IdHotel = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Idroom", room.IdRoom);
                    cmd.Parameters.AddWithValue("@Price", room.Price);
                    cn.Open();

                    result = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return room;
        }

        public List<Room> SearchRoomSimple(string location, int id)
        {
            List<Room> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room r INNER JOIN Hotel h ON r.IdHotel = h.IdHotel WHERE h.Location = @Location AND h.IdHotel = @id";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@id", id);


                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (double)(decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = (int)dr["IdHotel"];

                            results.Add(room);

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

        public List<int> SearchIdRoomSimple(string location)
        {
            List<int> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room,Hotel WHERE Location = @Location";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Location", location);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<int>();

                            int id = (int)dr["IdHotel"];

                            results.Add(id);

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

        public Room SearchRoomById(int id)
        {
            Room result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room WHERE IdRoom = @id";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                 
                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (double)(decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = (int)dr["IdHotel"];

                            result = room;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public List<Room> SearchEveryRooms()
        {
            List<Room> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room r INNER JOIN Hotel h ON r.IdHotel = h.IdHotel";

                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (double)(decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = (int)dr["IdHotel"];

                            results.Add(room);

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

        public List<Room> GetEveryRoomByIdHotel(int id)
        {
            List<Room> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room WHERE IdHotel = @id";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (double)(decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = (int)dr["IdHotel"];

                            results.Add(room);

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
