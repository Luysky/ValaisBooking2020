using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValaisBooking2020.Models
{
    public class RoomViewModel
    {
		public int Number { get; set; }
		public string Description { get; set; }
		public int Type { get; set; }
		public double Price { get; set; }
		public bool HasTV { get; set; }
		public bool HasHairDryer { get; set; }
	}
}
