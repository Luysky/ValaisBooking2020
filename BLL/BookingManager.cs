using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BookingManager : IBookingManager
    {
        public BookingManager(IConfiguration configuration)
        {
            BookingDB = new BookingsDB(configuration);
        }

        public IBookingsDB BookingDB { get; }

        public Bookings AddBooking(Bookings bookings)
        {
            return BookingDB.AddBooking(bookings);
        }
    }
}
