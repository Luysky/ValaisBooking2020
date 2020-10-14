using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
    }
}
