using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IPictureManager
    {
        IPictureDB PictureDB { get; }
        List<Picture> SearchListPicture(int idRoom);
    }
}
