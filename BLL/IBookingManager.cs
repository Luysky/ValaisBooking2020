using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IBookingManager
    {
        IBookingsDB BookingDB { get; }
        Bookings AddBooking(Bookings bookings);
        List<Bookings> GetAllReservation();
        List<Bookings> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut);
    }
}
