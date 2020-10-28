using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IBookingsDB
    {
        IConfiguration Configuration { get; }
        Bookings AddBooking(Bookings bookings);
        public List<Bookings> GetAllReservation(string location);
    }
}
