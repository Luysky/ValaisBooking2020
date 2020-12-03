 using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValaisBooking2020.Models
{
    public class SimpleSearchViewModel
    {

        public DateTime checkIn {get;set;}
        public DateTime checkOut { get; set; }

        public Location cities { get; set; }

        /*
        public IEnumerable<string> SelectedCities { get; set; }
        public IEnumerable<SelectListItem> CitiesResult { get; set; }
        */


    }
    public enum Location
    {
        Martigny,
        Sierre,
        Sion,
        Brig
    }



}
