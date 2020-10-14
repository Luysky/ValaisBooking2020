using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IBookingManager
    {
        IBookingDB BookingDB { get; }
        Bookings AddBooking(Bookings bookings);
    }
}
