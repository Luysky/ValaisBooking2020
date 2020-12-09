using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class BookingDB : IBookingDB
    {
        public IConfiguration Configuration { get; }

        public BookingDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Booking AddBooking(Booking bookings)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Bookings(Reference, CheckIn, CheckOut, Firstname, Lastname, Amount, IdRoom) values(@Reference,@CheckIn,@CheckOut,@Firstname,@Lastname, @Amount, @IdRoom); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Reference", bookings.Reference);
                    cmd.Parameters.AddWithValue("@CheckIn", bookings.CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", bookings.CheckOut);
                    cmd.Parameters.AddWithValue("@Firstname", bookings.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", bookings.Lastname);
                    cmd.Parameters.AddWithValue("@Amount", bookings.Amount);
                    cmd.Parameters.AddWithValue("@IdRoom", bookings.IdRoom);
                    
                    cn.Open();

                    bookings.IdBooking = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return bookings;
        }

        public List<Booking> GetAllReservation()
        {
            List<Booking> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Bookings WHERE IdRoom=IdRoom";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {

                            if (results == null)
                                results = new List<Booking>();

                            Booking bookings = new Booking();

                            bookings.IdBooking = (int)dr["IdBooking"];
                            bookings.Reference = (string)dr["Reference"];
                            bookings.CheckIn = (DateTime)dr["CheckIn"];
                            bookings.CheckOut = (DateTime)dr["CheckOut"];
                            bookings.Firstname = (string)dr["Firstname"];
                            bookings.Lastname = (string)dr["Lastname"];
                            bookings.Amount = (double)(decimal)dr["Amount"];
                            bookings.IdRoom = (int)dr["IdRoom"];

                            results.Add(bookings);
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

        public List<Booking> GetEveryReservation()
        {
            List<Booking> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Bookings";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            if (results == null)
                                results = new List<Booking>();

                            Booking bookings = new Booking();

                            bookings.IdBooking = (int)dr["IdBooking"];
                            bookings.Reference = (string)dr["Reference"];
                            bookings.CheckIn = (DateTime)dr["CheckIn"];
                            bookings.CheckOut = (DateTime)dr["CheckOut"];
                            bookings.Firstname = (string)dr["Firstname"];
                            bookings.Lastname = (string)dr["Lastname"];
                            bookings.Amount = (double)(decimal)dr["Amount"];
                            bookings.IdRoom = (int)dr["IdRoom"];

                            results.Add(bookings);
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

        public List<Booking> GetAllReservationDate(DateTime CheckIn, DateTime CheckOut)
        {
            List<Booking> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Bookings WHERE CheckIn BETWEEN @CheckIn AND @CheckOut";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", CheckOut);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            if (results == null)
                                results = new List<Booking>();

                            Booking bookings = new Booking();

                            bookings.IdBooking = (int)dr["IdBooking"];
                            bookings.Reference = (string)dr["Reference"];
                            bookings.CheckIn = (DateTime)dr["CheckIn"];
                            bookings.CheckOut = (DateTime)dr["CheckOut"];
                            bookings.Firstname = (string)dr["Firstname"];
                            bookings.Lastname = (string)dr["Lastname"];
                            bookings.Amount = (double)(decimal)dr["Amount"];
                            bookings.IdRoom = (int)dr["IdRoom"];

                            results.Add(bookings);
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

        public List<Booking> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut)
        {
            List<Booking> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Bookings WHERE IdRoom = IdRoom AND(CheckIn BETWEEN @CheckIn AND @CheckOut)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", CheckOut);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {

                            if (results == null)
                                results = new List<Booking>();

                            Booking bookings = new Booking();

                            bookings.IdBooking = (int)dr["IdBooking"];
                            bookings.Reference = (string)dr["Reference"];
                            bookings.CheckIn = (DateTime)dr["CheckIn"];
                            bookings.CheckOut = (DateTime)dr["CheckOut"];
                            bookings.Firstname = (string)dr["Firstname"];
                            bookings.Lastname = (string)dr["Lastname"];
                            bookings.Amount = (double)(decimal)dr["Amount"];
                            bookings.IdRoom = (int)dr["IdRoom"];

                            results.Add(bookings);
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

       

        public Booking UpdateBooking(string Reference, DateTime CheckIn, DateTime CheckOut)
        {
            
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Booking booking = new Booking();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Bookings SET CheckIn=@CheckIn, CheckOut=@CheckOut WHERE Reference=@Reference";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Reference", Reference);
                    cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", CheckOut);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return booking;
        }


        public int DeleteBooking(int idBooking)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Bookings WHERE IdBooking = @idBooking";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idBooking", idBooking);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }
}
