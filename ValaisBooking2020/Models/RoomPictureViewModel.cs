using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DTO;

namespace ValaisBooking2020.Models
{
    public class RoomPictureViewModel
    {
        public int IdRoom { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }       
        public List<Picture> Pictures { get; set; }
    }
}
