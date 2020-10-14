using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IBookingDB
    {
        IConfiguration Configuration { get; }
        Bookings AddBooking(Bookings bookings);
    }
}
