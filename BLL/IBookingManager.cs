using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IBookingManager
    {
        IBookingDB BookingDB { get; }
        IRoomDB RoomDB { get; }
        IHotelDB HotelDB { get; }
        IPictureDB PictureDB { get; }
        double CalculatePrice(double amount, DateTime checkIn, DateTime checkOut);
        Booking AddBooking(Booking booking);
        List<Booking> GetAllReservation();
        List<Booking> GetAllReservationDate(DateTime CheckIn, DateTime CheckOut);
        List<int> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
        void GetEveryBookings();
        List<int> GetBookingsWithRoomAndDates(DateTime checkIn, DateTime checkOut);
        List<Room> SearchSimple(List<int> listRoomBooked, string city);
        List<Room> SearchEveryAvailableRooms(List<int> listRoomBooked);
        void SearchAdvanced(List<Object> listCriteriaRoom, List<Object> listCriteriaHotel, DateTime checkIn, DateTime checkOut);
        Booking UpdateBooking(string Reference, DateTime CheckIn, DateTime CheckOut);
        int DeleteBooking(int idBooking);
    }
}
