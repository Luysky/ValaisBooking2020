using DTO;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IPictureManager
    {
        IPictureDB PictureDB { get; }
        List<Picture> SearchListPicture(int idRoom);
    }
}
