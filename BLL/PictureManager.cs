using DAL;
using DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class PictureManager : IPictureManager
    {
        public PictureManager(IConfiguration configuration)
        {
            PictureDB = new PictureDB(configuration);
        }

        public IPictureDB PictureDB { get; }

        public List<Picture> SearchListPicture(int idRoom)
        {
            return PictureDB.SearchListPicture(idRoom);
        }
    }
}
