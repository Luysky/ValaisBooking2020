using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IBookingsDB
    {
        IConfiguration Configuration { get; }
        Bookings AddBooking(Bookings bookings);
        List<Bookings> GetAllReservation();
        List<Bookings> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut);
        List<Bookings> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
    }
}
