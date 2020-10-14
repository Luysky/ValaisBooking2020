using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class BookingDB : IBookingDB
    {
        public IConfiguration Configuration { get; }

        public BookingDB(IConfiguration configuration)
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
                    string query = "Insert into Bookings(Reference, CheckIn, CheckOut, Firstname, Lastname, Amount, FK_Bookings_Room) values(@Reference,@CheckIn,@CheckOut,@Firstname,@Lastname, @Amount, @FK_Bookings_Room); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Reference", bookings.Reference);
                    cmd.Parameters.AddWithValue("@CheckIn", bookings.CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", bookings.CheckOut);
                    cmd.Parameters.AddWithValue("@Firstname", bookings.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", bookings.Lastname);
                    cmd.Parameters.AddWithValue("@Amount", bookings.Amount);
                    //cmd.Parameters.AddWithValue("@FK_Bookings_Room", bookings.);


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
    }
}
