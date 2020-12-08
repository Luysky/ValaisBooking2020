using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValaisBooking2020.Models
{
    public class AdvancedSearchViewModel
    {
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public Cities cities { get; set; }
        public int Category { get; set; }
        public bool HasWifi { get; set; }
        public bool HasParking { get; set; }
        public int type { get; set; }
        public bool HasTv { get; set; }
        public bool HasHairDryer { get; set; }
    }
    public enum Cities
    {
        Martigny,
        Sion,
        Brig
    }
}
