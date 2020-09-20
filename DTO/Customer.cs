using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Customer
    {
		public int Id { get; set; }
		[Required]
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }

		public override string ToString()
		{
			return $"{Id}|{Firstname}|{Lastname}|{CheckIn}|{CheckOut}";
		}

	}
}
