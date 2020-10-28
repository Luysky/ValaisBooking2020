using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace DAL
{
    public class PictureDB : IPictureDB
    {
        public IConfiguration Configuration { get; }

        public PictureDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Picture> SearchListPicture(int idRoom)
        {

            List<Picture> results = null;
            string ConnectionStrings = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionStrings))
                {
                    string query = "SELECT * FROM Picture WHERE IdRoom = @idRoom";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdRoom", idRoom);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {


                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Picture>();

                            Picture picture = new Picture();
                            picture.IdPicture = (int)dr["IdPicture"];
                            picture.Url = (string)dr["Url"];
                            picture.IdRoom = (int)dr["IdRoom"];

                            results.Add(picture);

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
