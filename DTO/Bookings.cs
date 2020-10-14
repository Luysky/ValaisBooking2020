using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Bookings
    {
		public int IdBooking { get; set; }
		[Required]
		public int Reference { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public double Amount { get; set; }
		public int IdRoom { get; set; }

		public override string ToString()
		{
			return $"{IdBooking}|{Reference}|{CheckIn}|{CheckOut}|{Firstname}|{Lastname}|{Amount}|{IdRoom}";
		}

	}
}
