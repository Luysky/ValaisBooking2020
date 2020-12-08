using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValaisBooking2020.Models
{
    public class BookingConfirmationViewModel
    {
		public string Reference { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public double Amount { get; set; }
		public int IdRoom { get; set; }

		public BookingConfirmationViewModel()
		{
        }
	}
}
