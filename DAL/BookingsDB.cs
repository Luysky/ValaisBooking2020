using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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

        public List<Bookings> GetAllReservation(string location)
        {
            List<Bookings> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings))
                {
                    string query = "SELECT * FROM Bookings WHERE Location=@location";
                    SqlCommand cmd = new SqlCommand(query, cn);

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
                            bookings.Amount = (double)dr["Amount"];
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
