using DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IPictureDB
    {

        IConfiguration Configuration { get; }
        List<Picture> SearchListPicture(int idRoom);

    }
}
