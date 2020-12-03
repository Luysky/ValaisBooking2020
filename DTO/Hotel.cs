using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Category { get; set; }
        public bool HasWifi { get; set; }
        public bool HasParking { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Website { get; set; }

        public override string ToString()
        {
            return $"{IdHotel}|{Name}|{Description}|{Location}|{Category.ToString()}|{HasWifi.ToString()}|{Phone}|{Email}|{Website}";
        }

        public string ShortInfo()
        {
            return $"Hotel: {Name} - {Location}";
        }

    }
}
