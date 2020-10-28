using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Room
    {
		public int IdRoom { get; set; }
		[Required]
		public int Number { get; set; }
		public string Description { get; set; }
		public int Type { get; set; }
		public double Price { get; set; }
		public bool HasTV { get; set; }
		public bool HasHairDryer { get; set; }
		public int IdHotel { get; set; }

		public override string ToString()
		{
			return $"{IdRoom}|{Number}|{Description}|{Type}|{Price}|{HasTV}|{HasHairDryer}|{IdHotel}";
		}

		public string ShortInfo()
        {
			return $"Chambre numéro: {Number} / CHF: {Price}.- / id hotel: {IdHotel}";
        }

	}
}
