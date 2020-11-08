using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IBookingDB
    {
        IConfiguration Configuration { get; }
        Booking AddBooking(Booking bookings, double amount);
        List<Booking> GetAllReservation();
        List<Booking> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut);
        List<Booking> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
        Booking UpdateBooking(string Reference, DateTime CheckIn, DateTime CheckOut);
        int DeleteBooking(int idBooking);
    }
}
