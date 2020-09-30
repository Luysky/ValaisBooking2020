using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
	public class BookingsDetails
	{
		public int IdBooking { get; set; }
		[Required]
		public int IdRoom { get; set; }
		[Required]
	}
}
