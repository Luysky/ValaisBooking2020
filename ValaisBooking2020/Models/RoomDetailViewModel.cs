using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValaisBooking2020.Models
{
    public class RoomDetailViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Category { get; set; }
        public bool HasWifi { get; set; }
        public bool HasParking { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public double Price { get; set; }
        public string Url { get; set; }
    }
}
