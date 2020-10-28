using DTO;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace DAL
{
    public interface IPictureDB
    {

        IConfiguration Configuration { get; }
        List<Picture> SearchListPicture(int idRoom);

    }
}
