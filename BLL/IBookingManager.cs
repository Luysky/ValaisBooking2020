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
        Booking AddBooking(Booking booking);
        List<Booking> GetAllReservation();
        List<Booking> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut);
        List<int> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
        void GetEveryBookings();
        List<int> GetBookingsWithRoomAndDates(int idRoom, DateTime checkIn, DateTime checkOut);
        void SearchSimple(List<int> listRoomBooked, string city);
        List<Room> SearchEveryAvailableRooms(List<int> listRoomBooked);
        void SearchAdvanced(HotelManager hotelManager, RoomManager roomManager, List<Object> listCriteriaRoom, List<Object> listCriteriaHotel, DateTime checkIn, DateTime checkOut);
        Booking UpdateBooking(Booking booking);
        int DeleteBooking(int idBooking);
    }
}
