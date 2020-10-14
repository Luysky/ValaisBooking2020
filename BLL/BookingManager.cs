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
            BookingDB = new BookingDB(configuration);
        }

        public IBookingDB BookingDB { get; }

        public Bookings AddBooking(Bookings bookings)
        {
            return BookingDB.AddBooking(bookings);
        }
    }
}
