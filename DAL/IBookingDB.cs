using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IBookingDB
    {
        IConfiguration Configuration { get; }
        Booking AddBooking(Booking bookings);
        List<Booking> GetAllReservation();
        List<Booking> GetAllReservationDate(DateTime CheckIn, DateTime CheckOut);
        List<Booking> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
        Booking UpdateBooking(string Reference, DateTime CheckIn, DateTime CheckOut);
        int DeleteBooking(string Reference);
        List<Booking> GetEveryReservation();
        Booking GetMyReservation(string reference, string firstname, string lastname);
    }
}
