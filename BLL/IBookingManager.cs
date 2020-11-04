using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IBookingManager
    {
        IBookingDB BookingDB { get; }
        Booking AddBooking(Booking booking);
        List<Booking> GetAllReservation();
        List<Booking> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut);
        List<int> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut);
        void GetEveryBookings(BookingManager bookingsDBManager);
        List<int> GetBookingsWithRoomAndDates(BookingManager bookingDBManager, int idRoom, DateTime checkIn, DateTime checkOut);
        void SearchSimple(RoomManager roomDBManager, List<int> listRoomBooked, HotelManager hotelDBManager, PictureManager pictureDBManager, string city);
        List<Room> SearchEveryAvailableRooms(RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, List<int> listRoomBooked);
        void SearchAdvanced(BookingManager bookingDBManager, RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, List<Object> listCriteriaRoom, List<Object> listCriteriaHotel, DateTime checkIn, DateTime checkOut);
    }
}
