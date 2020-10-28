using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace DAL
{
    public class BookingsDB : IBookingsDB
    {
        public IConfiguration Configuration { get; }

        public BookingsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Bookings AddBooking(Bookings bookings)
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

        public List<Bookings> GetAllReservation()
        {
            List<Bookings> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings))
                {
                    string query = "SELECT * FROM Bookings WHERE IdRoom=IdRoom";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    //cmd.Parameters.AddWithValue("@IdRoom", IdRoom);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        

                        while (dr.Read())
                        {

                            if (results == null)
                                results = new List<Bookings>();

                            Bookings bookings = new Bookings();

                            bookings.IdBooking = (int)dr["IdBooking"];
                            bookings.Reference = (int)dr["Reference"];
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

        public List<Bookings> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut)
        {
            List<Bookings> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings))
                {
                    string query = "SELECT * FROM Bookings WHERE IdRoom = @IdRoom AND(CheckIn BETWEEN @CheckIn AND @CheckOut)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdRoom", IdRoom);
                    cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", CheckOut);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {

                            if (results == null)
                                results = new List<Bookings>();

                            Bookings bookings = new Bookings();

                            bookings.IdBooking = (int)dr["IdBooking"];
                            bookings.Reference = (int)dr["Reference"];
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

    }
}
