using System.ComponentModel.DataAnnotations;


namespace DTO
{
    public class Picture
    {
		public int IdPicture { get; set; }
		[Required]
		public string Url { get; set; }
		public int IdRoom { get; set; }

		public override string ToString()
		{
			return $"{IdPicture}|{Url}|{IdRoom}";
		}
	}
}
