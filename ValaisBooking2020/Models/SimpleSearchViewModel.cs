using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValaisBooking2020.Models
{
    public class SimpleSearchViewModel
    {

        public IEnumerable<string> SelectedCities { get; set; }
        public IEnumerable<SelectListItem> CitiesResult { get; set; }
        /*
        public int CityId { get; set; }
        public Location CityName { get; set; }
        */
    }

    /*
    public enum Location
    {
        Martigny,
        Sierre,
        Sion,
        Brig
    }
    */
}
